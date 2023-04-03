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
  protected readonly MainDbContext dbContext;
  protected readonly DbSet<T> dbSet;

  public BaseRepository(MainDbContext dbContext)
  {
    this.dbContext = dbContext;
    this.dbSet = this.dbContext.Set<T>();
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
}
