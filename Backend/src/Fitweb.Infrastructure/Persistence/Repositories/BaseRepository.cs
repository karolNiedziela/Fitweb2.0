using Fitweb.Domain.Common;
using Fitweb.Domain.Filters;
using Fitweb.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly FitwebDbContext _context;

        public BaseRepository(FitwebDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
         => await _context
                    .Set<T>()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<(IEnumerable<T>, int TotalItems)> GetAllAsync(PaginationFilter pagination, string columName = null)
        {
            var queryable = _context.Set<T>().AsNoTracking();

            if (string.IsNullOrEmpty(columName))
                queryable = queryable.OrderBy(columName);


            var totalItems = await queryable.CountAsync();

            var data = await queryable
                        .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                        .Take(pagination.PageSize)
                        .ToListAsync();

             return (data, totalItems);
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
