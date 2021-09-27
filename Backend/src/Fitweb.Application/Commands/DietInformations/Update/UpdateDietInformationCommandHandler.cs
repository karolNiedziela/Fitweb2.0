using AutoMapper;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.DietInformations.Update
{
    public class UpdateDietInformationCommandHandler : IRequestHandler<UpdateDietInformationCommand>
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;

        public UpdateDietInformationCommandHandler(IAthleteRepository athleteRepository, IMapper mapper)
        {
            _athleteRepository = athleteRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDietInformationCommand request, CancellationToken cancellationToken)
        {
            var athlete = await _athleteRepository.GetDietInformations(request.UserId);

            var dietInformation = _mapper.Map<DietInformation>(request);
            athlete.UpdateDietInformation(dietInformation);

            await _athleteRepository.UpdateAsync(athlete);

            return Unit.Value;
        }
    }
}
