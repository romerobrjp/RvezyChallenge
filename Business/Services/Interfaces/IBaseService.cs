using Infra.Data.DTOs;
using Infra.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces;

public interface IBaseService<T> where T : BaseEntity
{
  Task<IEnumerable<T>> GetAll();
  Task<T> GetById(long id);
  Task<T> Create(T obj);
  Task<T> Update(long id, T obj);
  Task<bool> Delete(long id);
  FilterDTO ListFiltered(FilterDTO filter);
}
