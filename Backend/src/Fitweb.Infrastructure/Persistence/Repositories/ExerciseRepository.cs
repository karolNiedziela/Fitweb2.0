using Fitweb.Domain.Common;
using Fitweb.Domain.Exercises;
using Fitweb.Domain.Exercises.Repositories;
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
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(FitwebDbContext context) : base(context)
        {

        }

        public async Task<(IEnumerable<Exercise>, int TotalItems)> GetAll(PaginationFilter pagination)
        {
            var queryable = _context.Exercises.AsNoTracking();

            queryable = queryable.ApplyOrderBy("Information.Name", true);

            var totalItems = await queryable.CountAsync();

            var data = await queryable.ApplyPaging(pagination.PageSize, pagination.PageNumber);

            return (data, totalItems);
        }

        public async Task AddRangeAsync(List<Exercise> exercises)
        {
            await _context.Exercises.AddRangeAsync(exercises);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync()
            => await _context.Exercises.AnyAsync();
    }
}
