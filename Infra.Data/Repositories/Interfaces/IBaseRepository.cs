using Infra.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    T Insert(T obj);
    T Update(T obj);
    void Delete(int id);
    T SelectById(int id);
    IList<T> SelectAll();
    bool Exists(long? id);
}
