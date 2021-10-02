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

namespace Fitweb.Application.Commands.Athletes.Update
{
    public class UpdateAthleteCommandHandler : IRequestHandler<UpdateAthleteCommand, Response<string>>
    {
        private readonly IAthleteRepository _athleteRepository;

        public UpdateAthleteCommandHandler(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public async Task<Response<string>> Handle(UpdateAthleteCommand request, CancellationToken cancellationToken = default)
        {
            var athlete = await _athleteRepository.GetByUserId(request.UserId);

            athlete.Update(request.Height, request.Weight, request.NumberOfTrainings);

            await _athleteRepository.UpdateAsync(athlete);

            return Response.Updated(nameof(Athlete));
        }
    }
}
