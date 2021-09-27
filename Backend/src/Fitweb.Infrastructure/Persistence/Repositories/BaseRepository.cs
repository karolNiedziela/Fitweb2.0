using Fitweb.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            => await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null)
        {
            if (expression is not null)
            {
                return await _context.Set<T>().AnyAsync(expression);
            }

            return await _context.Set<T>().AnyAsync();
        }        
    }
}
