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
    public class DeleteDietInformationCommandHandler : IRequestHandler<DeleteDietInformationCommand>
    {
        private readonly IAthleteRepository _athleteRepository;

        public DeleteDietInformationCommandHandler(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public async Task<Unit> Handle(DeleteDietInformationCommand request, CancellationToken cancellationToken)
        {
            var athlete = await _athleteRepository.GetDietInformations(request.UserId);

            var toRemove = athlete.RemoveDietInformation(request.DietInformationId);

            await _athleteRepository.RemoveDietInformation(toRemove);

            return Unit.Value;
        }
    }
}
