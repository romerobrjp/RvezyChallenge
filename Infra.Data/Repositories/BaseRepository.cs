using Dapper;
using Infra.Data.Models;
using Infra.Data.Models.Context;
using Infra.Data.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly SqlServerContext context;
    protected readonly DbSet<T> dbset;
    protected readonly IConfiguration configuration;

    public T Insert(T obj)
    {
        try
        {
            dbset.Add(obj);
            context.SaveChanges();
            return obj;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public T Update(T obj)
    {
        if (!Exists(obj.Id)) return null;

        var result = dbset.AsNoTracking().SingleOrDefault(b => b.Id == obj.Id);

        if (result != null)
        {
            try
            {
                context.Entry(result).CurrentValues.SetValues(obj);
                context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        return result;
    }

    public void Delete(int id)
    {
        var result = dbset.SingleOrDefault(i => i.Id.Equals(id));

        try
        {
            if (result != null)
            {
                dbset.Remove(result);
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public T SelectById(int id)
    {
        return dbset.Find(id);
    }

    public IList<T> SelectAll()
    {
        return dbset.ToList<T>();
    }

    public bool Exists(long? id)
    {
        return dbset.Any(b => b.Id.Equals(id));
    }

    public EntityFilter ListFiltered(EntityFilter filter)
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
