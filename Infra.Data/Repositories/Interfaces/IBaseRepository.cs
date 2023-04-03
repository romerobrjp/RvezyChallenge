using Infra.Data.DTOs;
using Infra.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
  Task<T> Insert(T obj);
  Task<T> Update(long id, T obj);
  Task<bool> Delete(long id);
  Task<T> GetById(long id);
  IQueryable<T> GetAll();
  Task<bool> Exists(long? id);
  FilterDTO ListFiltered(FilterDTO filter);
}
