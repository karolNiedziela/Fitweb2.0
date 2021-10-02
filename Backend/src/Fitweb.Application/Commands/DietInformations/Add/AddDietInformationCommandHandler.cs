using AutoMapper;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.DietInformations.Add
{
    public class AddDietInformationCommandHandler : IRequestHandler<AddDietInformationCommand, Response<string>>
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IMapper _mapper;

        public AddDietInformationCommandHandler(IAthleteRepository athleteRepository, IMapper mapper)
        {
            _athleteRepository = athleteRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddDietInformationCommand request, CancellationToken cancellationToken = default)
        {
            var athlete = await _athleteRepository.GetDietInformations(request.UserId);
            var dietInformation = _mapper.Map<DietInformation>(request);

            athlete.AddDietInformation(dietInformation);

            await _athleteRepository.UpdateAsync(athlete);

            return Response.Added(nameof(DietInformation));
        }
    }
}
