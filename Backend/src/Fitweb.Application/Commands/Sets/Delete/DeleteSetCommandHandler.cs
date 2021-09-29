using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Sets.Delete
{
    public class DeleteSetCommandHandler : IRequestHandler<DeleteSetCommand>
    {
        private readonly ITrainingRepository _trainingRepository;

        public DeleteSetCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<Unit> Handle(DeleteSetCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetAllExercisesWithSets(request.UserId, request.TrainingId);

            var setToRemove = training.RemoveSet(request.ExerciseId, request.SetId);

            await _trainingRepository.RemoveSet(setToRemove);

            return Unit.Value;
        }
    }
}
