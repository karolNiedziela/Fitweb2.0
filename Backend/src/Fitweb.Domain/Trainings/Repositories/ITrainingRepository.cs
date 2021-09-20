using Fitweb.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Trainings.Repositories
{
    public interface ITrainingRepository : IBaseRepository<Training>
    {
        Task<Training> GetExercisesWithSets(string userId, int trainingId);

        Task<Training> GetExerciseWithSets(string userId, int trainingId, int exerciseId);

        Task RemoveTrainingExercise(TrainingExercise trainingExercise);

        Task RemoveSet(Set set);
    }
}
