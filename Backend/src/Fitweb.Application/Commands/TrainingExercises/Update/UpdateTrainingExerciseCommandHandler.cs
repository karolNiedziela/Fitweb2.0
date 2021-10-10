using AutoMapper;
using Fitweb.Application.Responses;
using Fitweb.Domain.Exceptions;
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
        private readonly IMapper _mapper;

        public UpdateTrainingExerciseCommandHandler(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateTrainingExerciseCommand request, CancellationToken cancellationToken = default)
        {
            var training = await _trainingRepository.GetExercisesWithSets(request.UserId, request.TrainingId, request.ExerciseId);
            if (training is null)
            {
                throw new NotFoundException(nameof(TrainingExercise), request.ExerciseId);
            }

            var sets = new List<Set>();
            foreach (var set in request.Sets)
            {
                sets.Add(new Set(set.Weight, set.NumberOfReps, set.NumberOfSets));
            }

            training.UpdateExercise(request.ExerciseId, sets);

            await _trainingRepository.UpdateAsync(training);

            return Response.Updated(nameof(TrainingExercise));
        }
    }
}
