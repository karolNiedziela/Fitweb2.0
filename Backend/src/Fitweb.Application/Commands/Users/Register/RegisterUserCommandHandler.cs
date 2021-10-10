using Fitweb.Application.Interfaces.Identity;
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

namespace Fitweb.Application.Commands.Users.Create
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Response<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly IAthleteRepository _athleteRepository;

        public RegisterUserCommandHandler(IIdentityService identityService, IAthleteRepository athleteRepository)
        {
            _identityService = identityService;
            _athleteRepository = athleteRepository;
        }

        public async Task<Response<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken = default)
        {
            var userId = await _identityService.CreateUserAsync(request.Username, request.Email, request.Password);

            var athlete = await _athleteRepository.GetByUserId(userId);
            if (athlete is not null)
            {
                throw new AlreadyExistsException(nameof(Athlete), $"with userId: '{userId}' already exists.", true);
            }

            athlete = new Athlete(userId);

            await _athleteRepository.AddAsync(athlete);

            return Response.Added("User");
        }
    }
}
