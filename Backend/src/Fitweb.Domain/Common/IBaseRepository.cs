using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Common
{
    public interface IBaseRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);

        Task RemoveAsync(T entity);

        Task UpdateAsync(T entity);
    }
}
