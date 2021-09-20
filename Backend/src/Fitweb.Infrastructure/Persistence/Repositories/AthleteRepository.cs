using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Filters;
using Fitweb.Domain.Trainings;
using Fitweb.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Repositories
{
    public class AthleteRepository : BaseRepository<Athlete>, IAthleteRepository
    {
        public AthleteRepository(FitwebDbContext context) : base(context)
        {
        }

        public async Task<int> GetAthleteId(string userId)
            => await _context.Athletes.Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefaultAsync();

        public async Task<Athlete> GetByUserId(string userId)
            => await _context.Athletes.SingleOrDefaultAsync(x => x.UserId == userId);

        public async Task<Athlete> GetTrainings(int athleteId)
            => await _context.Athletes
                .Include(x => x.Trainings)
                    .ThenInclude(x => x.Exercises)
                    .ThenInclude(x => x.Sets)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == athleteId);

        public async Task<(IEnumerable<Training>, int TotalItems)> GetPagedTrainings(int athleteId,
            PaginationFilter pagination)
        {
            var queryable = _context.Trainings.Where(x => x.AthleteId == athleteId).AsNoTracking();

            queryable = queryable.ApplyOrderBy("Day", true);


            var totalItems = await queryable.CountAsync();

            var data = await queryable.ApplyPaging(pagination.PageSize, pagination.PageNumber);

            return (data, totalItems);
        }
    }
}
