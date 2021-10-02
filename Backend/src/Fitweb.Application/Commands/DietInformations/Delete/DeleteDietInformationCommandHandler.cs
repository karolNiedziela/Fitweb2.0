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

namespace Fitweb.Application.Commands.DietInformations.Delete
{
    public class DeleteDietInformationCommandHandler : IRequestHandler<DeleteDietInformationCommand, Response<string>>
    {
        private readonly IAthleteRepository _athleteRepository;

        public DeleteDietInformationCommandHandler(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public async Task<Response<string>> Handle(DeleteDietInformationCommand request, CancellationToken cancellationToken = default)
        {
            var athlete = await _athleteRepository.GetDietInformations(request.UserId);

            var toRemove = athlete.RemoveDietInformation(request.DietInformationId);

            await _athleteRepository.RemoveDietInformation(toRemove);

            return Response.Deleted(nameof(DietInformation));
        }
    }
}
