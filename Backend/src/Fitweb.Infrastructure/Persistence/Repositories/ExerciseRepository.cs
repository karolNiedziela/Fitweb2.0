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

        public async Task<(IEnumerable<Exercise>, int TotalItems)> GetAll(PaginationFilter pagination, 
            string searchName = null, PartOfBody? partOfBody = null)
        {
            var queryable = _context.Exercises.AsNoTracking();

            if (!string.IsNullOrEmpty(searchName))
            {
                queryable = queryable.Where(x => x.Information.Name.Contains(searchName));
            }

            if (partOfBody.HasValue)
            {
                queryable = queryable.Where(x => x.PartOfBody == partOfBody.Value);
            }

            queryable = queryable.OrderBy(x => x.Information.Name);

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
