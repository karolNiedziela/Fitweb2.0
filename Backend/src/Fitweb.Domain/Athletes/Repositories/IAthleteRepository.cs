using Fitweb.Domain.Common;
using Fitweb.Domain.Filters;
using Fitweb.Domain.Trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Athletes.Repositories
{
    public interface IAthleteRepository : IBaseRepository<Athlete>
    {
        Task<int> GetAthleteId(string userId);

        Task<Athlete> GetByUserId(string userId);

        Task<Athlete> GetTrainings(int athleteId);

        Task<(IEnumerable<Training>, int TotalItems)> GetPagedTrainings(int athleteId, PaginationFilter pagination);
    }
}
