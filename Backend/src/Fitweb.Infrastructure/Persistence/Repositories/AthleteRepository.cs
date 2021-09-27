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

        public async Task<Athlete> GetTrainings(string userId)
            => await _context.Athletes
                .Include(x => x.Trainings)
                    .ThenInclude(x => x.Exercises)
                    .ThenInclude(x => x.Sets)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserId == userId);

        public async Task<Athlete> GetDietInformations(string userId)
            => await _context.Athletes
                .Include(x => x.DietInformations)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserId == userId);

        public async Task<Athlete> GetFoodProducts(string userId)
            => await _context.Athletes
                .Include(x => x.FoodProducts)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserId == userId);

        public async Task<(IEnumerable<Training>, int TotalItems)> GetPagedTrainings(string userId,
            PaginationFilter pagination)
        {
            var queryable = _context.Trainings
                .Include(x => x.Athlete)
                .Where(x => x.Athlete.UserId == userId)
                .AsNoTracking();

            queryable = queryable.ApplyOrderBy("Day", true);

            var totalItems = await queryable.CountAsync();

            var data = await queryable.ApplyPaging(pagination.PageSize, pagination.PageNumber);

            return (data, totalItems);
        }

        public async Task RemoveDietInformation(DietInformation dietInformation)
        {
            _context.DietInformations.Remove(dietInformation);

            await _context.SaveChangesAsync();
        }
    }
}
