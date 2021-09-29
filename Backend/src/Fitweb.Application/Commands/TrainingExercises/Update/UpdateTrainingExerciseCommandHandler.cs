using Fitweb.Application.Responses;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Exercises;
using Fitweb.Domain.Exercises.Repositories;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Update
{
    public class UpdateTrainingExerciseCommandHandler : IRequestHandler<UpdateTrainingExerciseCommand, Response<string>>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public UpdateTrainingExerciseCommandHandler(ITrainingRepository trainingRepository, IExerciseRepository exerciseRepository)
        {
            _trainingRepository = trainingRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<Response<string>> Handle(UpdateTrainingExerciseCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetAllExercisesWithSets(request.UserId, request.TrainingId);
            if (training is null)
            {
                throw new NotFoundException(nameof(Training), request.TrainingId);
            }

            var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);
            if (exercise is null)
            {
                throw new NotFoundException(nameof(Exercise), request.ExerciseId);
            }

            var newExercise = await _exerciseRepository.GetByIdAsync(request.NewExerciseId);
            if (newExercise is null)
            {
                throw new NotFoundException(nameof(Exercise), request.NewExerciseId);
            }

            training.UpdateExercise(request.ExerciseId, newExercise);

            await _trainingRepository.UpdateAsync(training);

            return Response.Updated(nameof(TrainingExercise));
        }
    }
}
