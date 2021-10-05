using Fitweb.Application.Responses;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Sets.Update
{
    public class UpdateSetCommandHandler : IRequestHandler<UpdateSetCommand, Response<string>>
    {
        private readonly ITrainingRepository _trainingRepository;

        public UpdateSetCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<Response<string>> Handle(UpdateSetCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository
                .GetExercisesWithSets(request.UserId, request.TrainingId, request.ExerciseId);
            if (training is null)
            {
                throw new NotFoundException(nameof(Training), request.TrainingId);
            }

            training.UpdateSet(request.ExerciseId, request.SetId, request.Weight, request.NumberOfReps, request.NumberOfSets);

            await _trainingRepository.UpdateAsync(training);

            return Response.Updated(nameof(Set));
        }
    }
}
