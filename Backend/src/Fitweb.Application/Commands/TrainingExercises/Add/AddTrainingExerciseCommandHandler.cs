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
using Fitweb.Domain.Exercises.Repositories;
using Fitweb.Application.Responses;
using AutoMapper;

namespace Fitweb.Application.Commands.TrainingExercises.Add
{
    public class AddTrainingExerciseCommandHandler : IRequestHandler<AddTrainingExerciseCommand, Response<string>>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public AddTrainingExerciseCommandHandler(ITrainingRepository trainingRepository, IExerciseRepository exerciseRepository)
        {
            _trainingRepository = trainingRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<Response<string>> Handle(AddTrainingExerciseCommand request, CancellationToken cancellationToken = default)
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

            var sets = new List<Set>();
            foreach (var set in request.Sets)
            {
                sets.Add(new Set(set.Weight, set.NumberOfReps, set.NumberOfSets));
            }

            training.AddExercise(exercise, sets);

            await _trainingRepository.UpdateAsync(training);

            return Response.Added(nameof(TrainingExercise));
        }
    }
}
