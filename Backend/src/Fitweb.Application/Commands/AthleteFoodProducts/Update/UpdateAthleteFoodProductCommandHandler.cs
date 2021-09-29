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

namespace Fitweb.Application.Commands.AthleteFoodProducts.Update
{
    public class UpdateAthleteFoodProductCommandHandler : IRequestHandler<UpdateAthleteFoodProductCommand, Response<string>>
    {
        private readonly IAthleteRepository _athleteRepository;

        public UpdateAthleteFoodProductCommandHandler(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public async Task<Response<string>> Handle(UpdateAthleteFoodProductCommand request, CancellationToken cancellationToken)
        {
            var athlete = await _athleteRepository.GetFoodProducts(request.UserId);
            if (athlete is null)
            {
                throw new NotFoundException(nameof(Athlete), request.UserId, KeyType.UserId);
            }

            athlete.UpdateFoodProduct(request.AthleteFoodProductId, request.Weight);

            await _athleteRepository.UpdateAsync(athlete);

            return Response.Updated(nameof(AthleteFoodProduct));
        }
    }
}
