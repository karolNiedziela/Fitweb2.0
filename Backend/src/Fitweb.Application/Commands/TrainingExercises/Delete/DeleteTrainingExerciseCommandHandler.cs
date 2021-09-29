using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Delete
{
    public class DeleteTrainingExerciseCommandHandler : IRequestHandler<DeleteTrainingExerciseCommand, Response<string>>
    {
        private readonly ITrainingRepository _trainingRepository;

        public DeleteTrainingExerciseCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<Response<string>> Handle(DeleteTrainingExerciseCommand request, CancellationToken cancellationToken = default)
        {
            var training = await _trainingRepository.GetAllExercisesWithSets(request.UserId, request.TrainingId);

            var exerciseToRemove = training.RemoveExercise(request.ExerciseId);

            await _trainingRepository.RemoveTrainingExercise(exerciseToRemove);

            return Response.Deleted(nameof(TrainingExercise));
        }
    }
}
