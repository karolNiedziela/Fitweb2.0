using Fitweb.Application.Exceptions;
using Fitweb.Application.Interfaces;
using Fitweb.Application.Interfaces.Identity;
using Fitweb.Application.DTO;
using Fitweb.Infrastructure.Identity.Entities;
using Fitweb.Infrastructure.Identity.Exceptions;
using Fitweb.Infrastructure.Identity.Factories;
using Fitweb.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenFactory _refreshTokenFactory;
        private readonly UserManager<User> _userManager;
        private readonly IJwtHandler _jwtHandler;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenFactory refreshTokenFactory, UserManager<User> userManager,IJwtHandler jwtHandler, 
            IDateTimeProvider dateTimeProvider, IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenFactory = refreshTokenFactory;
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _dateTimeProvider = dateTimeProvider;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthDto> UseAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetAsync(refreshToken);

            if (token is null)
            {
                throw new InvalidRefreshTokenException();
            }

            var user = await _userManager.FindByNameAsync(token.Username);

            if (user is null)
            {
                throw new NotFoundException(user.GetType().Name, token.Username);
            }

            token.Use(_dateTimeProvider.Now);
            var userRoles = await _userManager.GetRolesAsync(user);
            var jwt = _jwtHandler.Create(user.Id, user.UserName, userRoles); 

            var newRefreshToken = _refreshTokenFactory.Create(user.UserName);
            jwt.RefreshToken = newRefreshToken.Token;

            await _refreshTokenRepository.AddAsync(newRefreshToken);
            await _refreshTokenRepository.UpdateAsync(token);

            return jwt;
        }

        public async Task RevokeAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetAsync(refreshToken);

            if (token is null)
            {
                throw new InvalidRefreshTokenException();
            }

            token.RevokedAt = _dateTimeProvider.Now;
            await _refreshTokenRepository.UpdateAsync(token);
        }


    }
}
