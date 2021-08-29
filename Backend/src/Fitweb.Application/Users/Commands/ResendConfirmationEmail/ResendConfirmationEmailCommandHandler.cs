using Fitweb.Application.Interfaces.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Users.Commands.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand>
    {
        private readonly IIdentityService _identityService;

        public ResendConfirmationEmailCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            await _identityService.ResendEmailConfirmationAsync(request.Email);

            return Unit.Value;
        }
    }
}
