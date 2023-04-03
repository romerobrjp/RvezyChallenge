using Business.Services.Interfaces;
using Infra.Data.Models;
using Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services;

public class BaseService<T> : IBaseService<T> where T : BaseEntity
{
  protected readonly IBaseRepository<T> _repository;
  private readonly string INVALID_ID_MSG = "ID can't be zero.";

  public BaseService(IBaseRepository<T> repository)
  {
    _repository = repository;
  }

  public async Task<T> GetById(long id)
  {
    if (id == 0)
      throw new ArgumentException(INVALID_ID_MSG);

    return await _repository.GetById(id);
  }

  public async Task<IEnumerable<T>> GetAll() => await _repository.GetAll().ToListAsync();

  public virtual async Task<T> Create(T obj) => await _repository.Insert(obj);

  public async Task<T> Update(long id, T obj) => await _repository.Update(id, obj);

  public async Task<bool> Delete(long id)
  {
    if (id == 0)
      throw new ArgumentException(INVALID_ID_MSG);

    return await _repository.Delete(id);
  }
}
