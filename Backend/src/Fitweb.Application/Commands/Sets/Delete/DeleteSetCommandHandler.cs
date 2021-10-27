using Fitweb.Application.Responses;
using Fitweb.Domain.Trainings;
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
    public class DeleteSetCommandHandler : IRequestHandler<DeleteSetCommand, Response<string>>
    {
        private readonly ITrainingRepository _trainingRepository;

        public DeleteSetCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<Response<string>> Handle(DeleteSetCommand request, CancellationToken cancellationToken = default)
        {
            var training = await _trainingRepository.GetExercisesWithSets(request.UserId, request.TrainingId, request.ExerciseId);

            var setToRemove = training.RemoveSet(request.ExerciseId, request.SetId);

            await _trainingRepository.RemoveSet(setToRemove);

            return Response.Deleted(nameof(Set));
        }
    }
}
