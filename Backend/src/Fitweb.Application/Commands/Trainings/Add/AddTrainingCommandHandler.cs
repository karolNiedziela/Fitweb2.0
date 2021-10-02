using AutoMapper;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Trainings;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Fitweb.Application.Responses;

namespace Fitweb.Application.Commands.Trainings.Add
{
    public class AddTrainingCommandHandler : IRequestHandler<AddTrainingCommand, Response<string>>
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;

        public AddTrainingCommandHandler(IAthleteRepository athleteRepository, IMapper mapper)
        {
            _athleteRepository = athleteRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddTrainingCommand request, CancellationToken cancellationToken = default)
        {
            var athlete = await _athleteRepository.GetByUserId(request.UserId);

            var training = _mapper.Map<Training>(request);

            athlete.AddTraining(training);
            await _athleteRepository.UpdateAsync(athlete);

            return Response.Added(nameof(Training));
        }
    }
}
