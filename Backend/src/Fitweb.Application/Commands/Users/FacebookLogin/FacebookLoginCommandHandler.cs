using Fitweb.Application.DTO;
using Fitweb.Application.Interfaces.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Users.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommand, AuthDto>
    {
        private readonly IIdentityService _identityService;

        public FacebookLoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AuthDto> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
        {
            var token = await _identityService.FacebookLoginAsync(request.AccessToken);

            return token;
        }
    }
}
