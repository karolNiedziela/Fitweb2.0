using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Trainings.Delete
{
    public class DeleteTrainingCommandHandler : IRequestHandler<DeleteTrainingCommand, Response<string>>
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly ITrainingRepository _trainingRepository;

        public DeleteTrainingCommandHandler(IAthleteRepository athleteRepository, 
            ITrainingRepository trainingRepository)
        {
            _athleteRepository = athleteRepository;
            _trainingRepository = trainingRepository;
        }

        public async Task<Response<string>> Handle(DeleteTrainingCommand request, CancellationToken cancellationToken = default)
        {
            var athlete = await _athleteRepository.GetTrainings(request.UserId);

            var toRemove = athlete.RemoveTraining(request.Id);

            await _trainingRepository.RemoveAsync(toRemove);

            return Response.Deleted(nameof(Training));
        }
    }
}
