using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Delete
{
    public class DeleteTrainingExerciseCommandHandler : IRequestHandler<DeleteTrainingExerciseCommand>
    {
        private readonly ITrainingRepository _trainingRepository;

        public DeleteTrainingExerciseCommandHandler(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<Unit> Handle(DeleteTrainingExerciseCommand request, CancellationToken cancellationToken)
        {
            var training = await _trainingRepository.GetExercisesWithSets(request.UserId, request.TrainingId);

            var exerciseToRemove = training.RemoveExercise(request.ExerciseId);

            await _trainingRepository.RemoveTrainingExercise(exerciseToRemove);

            return Unit.Value;
        }
    }
}
