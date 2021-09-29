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
using Fitweb.Application.Responses;

namespace Fitweb.Application.Commands.Athletes.Create
{
    public class CreateAthleteCommandHandler : IRequestHandler<CreateAthleteCommand, Response<string>>
    {
        private readonly IAthleteRepository _athleteRepository;

        public CreateAthleteCommandHandler(IAthleteRepository athleteRepository)
        {
            _athleteRepository = athleteRepository;
        }

        public async Task<Response<string>> Handle(CreateAthleteCommand request, CancellationToken cancellationToken = default)
        {
            var athlete = await _athleteRepository.GetByUserId(request.UserId);
            if (athlete is not null)
            {
                throw new AlreadyExistsException(nameof(Athlete), $"with userId: '{request.UserId}' already exists.", true);
            }

            athlete = new Athlete(request.UserId, request.Height, request.Weight, request.NumberOfTrainings);

            await _athleteRepository.AddAsync(athlete);

            return Response.Added(nameof(Athlete));
        }
    }
}
