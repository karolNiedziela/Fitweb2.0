using Fitweb.Application.DTO;
using Fitweb.Application.Interfaces.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.RefreshTokens.UseToken
{
    public class UseTokenCommandHandler : IRequestHandler<UseTokenCommand, AuthDto>
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public UseTokenCommandHandler(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        public async Task<AuthDto> Handle(UseTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _refreshTokenService.UseAsync(request.RefreshToken);

            return token;
        }
    }
}
