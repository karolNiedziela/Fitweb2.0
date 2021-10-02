using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.AthleteFoodProducts.Delete
{
    public class DeleteAthleteFoodProductCommandHandler : IRequestHandler<DeleteAthleteFoodProductCommand, Response<string>>
    {
        private readonly IAthleteRepository _athleteRepository;

        public DeleteAthleteFoodProductCommandHandler(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public async Task<Response<string>> Handle(DeleteAthleteFoodProductCommand request, CancellationToken cancellationToken = default)
        {
            var athlete = await _athleteRepository.GetFoodProducts(request.UserId);

            athlete.RemoveFoodProduct(request.AthleteFoodProductId);

            await _athleteRepository.UpdateAsync(athlete);

            return Response.Deleted(nameof(AthleteFoodProduct));
        }
    }
}
