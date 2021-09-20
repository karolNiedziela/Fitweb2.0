using AutoMapper;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Trainings;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Trainings.Add
{
    public class AddTrainingCommandHandler : IRequestHandler<AddTrainingCommand>
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;

        public AddTrainingCommandHandler(IAthleteRepository athleteRepository, IMapper mapper)
        {
            _athleteRepository = athleteRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddTrainingCommand request, CancellationToken cancellationToken)
        {
            var athlete = await _athleteRepository.GetByUserId(request.UserId);
            if (athlete is null)
            {
                throw new NotFoundException(nameof(Athlete), request.UserId);
            }

            var training = _mapper.Map<Training>(request);

            athlete.AddTraining(training);
            await _athleteRepository.UpdateAsync(athlete);

            return Unit.Value;
        }
    }
}
