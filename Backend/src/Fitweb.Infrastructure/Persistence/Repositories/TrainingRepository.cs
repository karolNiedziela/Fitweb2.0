using Fitweb.Application.Interfaces;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
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

        public async Task<Training> GetExercisesWithSets(string userId, int trainingId)
        {
            return await _context.Athletes
                    .AsNoTracking()
                    .Include(x => x.Trainings.Where(x => x.Id == trainingId))
                        .ThenInclude(x => x.Exercises)
                            .ThenInclude(x => x.Sets)
                    .Include(x => x.Trainings)
                        .ThenInclude(x => x.Exercises)
                            .ThenInclude(x => x.Exercise)
                    .Where(x => x.UserId == userId)
                    .Select(x => x.Trainings.FirstOrDefault())
                    .FirstOrDefaultAsync();
        }

        public async Task<Training> GetExerciseWithSets(string userId, int trainingId, int exerciseId)
        {
            return await _context.Athletes
                            .AsNoTracking()
                            .Include(x => x.Trainings.Where(x => x.Id == trainingId))
                                .ThenInclude(x => x.Exercises.Where(x => x.ExerciseId == exerciseId))
                                    .ThenInclude(x => x.Sets)
                            .Select(x => x.Trainings.FirstOrDefault())
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
