using Fitweb.Domain.Filters;
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

        Task<IEnumerable<T>> GetAllAsync();

        Task<(IEnumerable<T>, int TotalItems)> GetAllAsync(PaginationFilter pagination, string columName = null);

        Task AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);
    }
}
