using Dapper;
using Infra.Data.DTOs;
using Infra.Data.Models;
using Infra.Data.Models.Context;
using Infra.Data.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
  protected readonly MainDbContext dbContext;
  protected readonly DbSet<T> dbSet;
  protected readonly IConfiguration configuration;

  public BaseRepository(MainDbContext dbContext, IConfiguration configuration)
  {
    this.dbContext = dbContext;
    this.dbSet = this.dbContext.Set<T>();
    this.configuration = configuration; 
  }

  public async Task<T> Insert(T obj)
  {
    try
    {
      await this.dbSet.AddAsync(obj);
      await this.dbContext.SaveChangesAsync();
      return obj;
    }
    catch (Exception)
    {
      throw;
    }
  }

  public async Task<T> Update(long id, T obj)
  {
    var result = await dbSet.AsNoTracking().SingleOrDefaultAsync(b => b.Id == id);

    if (result != null)
    {
      try
      {
        dbContext.Entry(result).CurrentValues.SetValues(obj);
        dbContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        dbContext.SaveChanges();
      }
      catch (Exception)
      {
        throw new Exception($"Couldn't update entity. ID {obj.Id} not found in database.");
      }
    }

    return result;
  }

  public async Task<bool> Delete(long id)
  {
    var result = dbSet.SingleOrDefault(i => i.Id.Equals(id));

    try
    {
      if (result != null)
      {
        dbSet.Remove(result);
        return await dbContext.SaveChangesAsync() > 0;
      }

      return false;
    }
    catch (Exception)
    {
      throw new Exception($"Couldn't delete entity: ID {id} not found in database.");
    }
  }

  public async Task<T> GetById(long id) => await dbSet.FindAsync(id);

  public IQueryable<T> GetAll() => dbSet.AsQueryable<T>();

  public async Task<bool> Exists(long? id) => await dbSet.AnyAsync(b => b.Id.Equals(id));

  public FilterDTO ListFiltered(FilterDTO filter)
  {
    string query = "";
    string sqlSelect = $"SELECT * FROM {filter.TableName} ";
    string sqlWhere = "";
    string sqlOrderBy = $"ORDER BY {filter.SortField} {filter.SortOrder} ";
    string sqlOffset = $"OFFSET {filter.Offset} ROWS ";
    string sqlLimit = $"FETCH NEXT {filter.PageSize} ROWS ONLY";

    if (filter.FieldsDictionary.Count > 0)
    {
      sqlWhere += "WHERE ";

      for (int i = 0; i < filter.FieldsDictionary.Count; i++)
      {
        string propName = filter.FieldsDictionary.Keys.ElementAt(i);
        string propValue = filter.FieldsDictionary[filter.FieldsDictionary.Keys.ElementAt(i)].ToString();

        if (propValue.GetType() == typeof(string))
        {
          sqlWhere = String.Concat(sqlWhere, ($"{propName} LIKE '%{propValue}%' "));
        }
        else
        {
          sqlWhere = String.Concat(sqlWhere, ($"{propName} = {propValue} "));
        }

        if (i + 1 < filter.FieldsDictionary.Count)
        {
          sqlWhere = String.Concat(sqlWhere, " AND ");
        }
      }
    }

    query = sqlSelect + sqlWhere + sqlOrderBy + sqlOffset + sqlLimit;

    using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
    {
      conn.Open();

      filter.Rows = conn.Query<T>(query).AsList();

      conn.Close();

      return filter;
    }
  }
}
