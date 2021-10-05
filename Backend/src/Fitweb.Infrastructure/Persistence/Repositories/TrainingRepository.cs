using Fitweb.Application.Interfaces;
using Fitweb.Domain.Filters;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using Fitweb.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Repositories
{
    public class TrainingRepository : BaseRepository<Training>, ITrainingRepository
    { 
        public TrainingRepository(FitwebDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Training>, int TotalItems)> GetPagedTrainings(string userId,
            PaginationFilter pagination, DateTime? date = null)
        {
            var queryable = _context.Trainings
                .Include(x => x.Athlete)
                .Where(x => x.Athlete.UserId == userId)
                .AsNoTracking();

            if (date.HasValue)
            {
                queryable = queryable.Where(x => x.Date == date.Value.Date);
            }

            queryable = queryable.ApplyOrderBy("Day", true);

            var totalItems = await queryable.CountAsync();

            var data = await queryable.ApplyPaging(pagination.PageSize, pagination.PageNumber);

            return (data, totalItems);
        }

        public async Task<Training> GetExercisesWithSets(string userId, int trainingId, int? exerciseId = null)
        {
            if (exerciseId.HasValue)
            {
                return await _context.Trainings
                        .Include(x => x.Athlete)
                        .Include(x => x.Exercises.Where(x => x.ExerciseId == exerciseId.Value))
                            .ThenInclude(x => x.Sets)
                        .Where(x => x.Id == trainingId && x.Athlete.UserId == userId)
                        .FirstOrDefaultAsync();
            }

            return await _context.Trainings
                        .Include(x => x.Athlete)
                        .Include(x => x.Exercises)
                            .ThenInclude(x => x.Sets)
                        .Include(x => x.Exercises)
                            .ThenInclude(x => x.Exercise)
                        .Where(x => x.Id == trainingId && x.Athlete.UserId == userId)
                        .FirstOrDefaultAsync();     
        }

        public async Task RemoveTrainingExercise(TrainingExercise trainingExercise)
        {
            _context.TrainingExercises.Remove(trainingExercise);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSet(Set set)
        {
            _context.Sets.Remove(set);
            await _context.SaveChangesAsync();
        }
    }
}
