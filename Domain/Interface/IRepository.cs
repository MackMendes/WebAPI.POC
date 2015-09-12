using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);

        Task<ICollection<T>> GetAll();

        Task<ICollection<T>> GetAll(Func<T, bool> functionExpress);

        Task<bool> Insert(T entity);

        Task<bool> Alter(T entity);

        Task<bool> Delete(T Entity);
    }
}
