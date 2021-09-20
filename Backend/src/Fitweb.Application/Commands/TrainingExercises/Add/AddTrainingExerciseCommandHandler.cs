using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Common;
using Fitweb.Domain.Exercises;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Add
{
    public class AddTrainingExerciseCommandHandler : IRequestHandler<AddTrainingExerciseCommand>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IBaseRepository<Exercise> _exerciseRepository;

        public AddTrainingExerciseCommandHandler(ITrainingRepository trainingRepository, IBaseRepository<Exercise> exerciseRepository)
        {
            _trainingRepository = trainingRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<Unit> Handle(AddTrainingExerciseCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetExercisesWithSets(request.UserId, request.TrainingId);
            if (training is null)
            {
                throw new NotFoundException(nameof(Training), request.TrainingId);
            }

            var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);
            if (exercise is null)
            {
                throw new NotFoundException(nameof(Exercise), request.ExerciseId);
            }
            training.AddExercise(exercise);

            await _trainingRepository.UpdateAsync(training);

            return Unit.Value;
        }
    }
}
