using AutoMapper;
using Fitweb.Domain.Exceptions;
using Fitweb.Application.Interfaces;
using Fitweb.Application.Requests;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Athletes.Create
{
    public class CreateAthleteCommandHandler : IRequestHandler<CreateAthleteCommand>
    {
        private readonly IAthleteRepository _athleteRepository;

        public CreateAthleteCommandHandler(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public async Task<Unit> Handle(CreateAthleteCommand request, CancellationToken cancellationToken)
        {
            var athlete = await _athleteRepository.GetByUserId(request.UserId);
            if (athlete is not null)
            {
                throw new AlreadyExistsException(nameof(Athlete), request.UserId);
            }

            athlete = new Athlete(request.UserId, request.Height, request.Weight, request.NumberOfTrainings);

            await _athleteRepository.AddAsync(athlete);

            return Unit.Value;
        }
    }
}
