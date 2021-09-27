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

        Task<Athlete> GetTrainings(string userId);

        Task<Athlete> GetDietInformations(string userId);

        Task<Athlete> GetFoodProducts(string userId);

        Task<(IEnumerable<Training>, int TotalItems)> GetPagedTrainings(string userId, PaginationFilter pagination);

        Task RemoveDietInformation(DietInformation dietInformation);
    }
}
