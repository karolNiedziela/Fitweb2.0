using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Trainings.Delete
{
    public class DeleteTrainingCommandHandler : IRequestHandler<DeleteTrainingCommand>
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly ITrainingRepository _trainingRepository;

        public DeleteTrainingCommandHandler(IAthleteRepository athleteRepository, 
            ITrainingRepository trainingRepository)
        {
            _athleteRepository = athleteRepository;
            _trainingRepository = trainingRepository;
        }

        public async Task<Unit> Handle(DeleteTrainingCommand request, CancellationToken cancellationToken)
        {
            var athleteId = await _athleteRepository.GetAthleteId(request.UserId);
            var athlete = await _athleteRepository.GetTrainings(athleteId);

            var toRemove = athlete.RemoveTraining(request.TrainingId);

            await _trainingRepository.RemoveAsync(toRemove);

            return Unit.Value;
        }
    }
}
