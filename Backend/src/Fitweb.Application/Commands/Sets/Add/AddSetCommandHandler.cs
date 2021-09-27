using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Sets.Add
{
    public class AddSetCommandHandler : IRequestHandler<AddSetCommand>
    {
        private readonly ITrainingRepository _trainingRepository;

        public AddSetCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<Unit> Handle(AddSetCommand request, CancellationToken cancellationToken)
        {
            var trainingWithSets = await _trainingRepository
                .GetExerciseWithSets(request.UserId, request.TrainingId, request.ExerciseId);

            trainingWithSets.AddSet(request.ExerciseId, new Set(request.Weight, request.NumberOfReps, request.NumberOfSets));

            await _trainingRepository.UpdateAsync(trainingWithSets);

            return Unit.Value;
        }
    }
}
