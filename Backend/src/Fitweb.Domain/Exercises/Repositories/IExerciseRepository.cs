using Fitweb.Domain.Common;
using Fitweb.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exercises.Repositories
{
    public interface IExerciseRepository : IBaseRepository<Exercise>
    {
        Task<(IEnumerable<Exercise>, int TotalItems)> GetAll(PaginationFilter pagination);

        Task AddRangeAsync(List<Exercise> exercises);

        Task<bool> AnyAsync();
    }
}
