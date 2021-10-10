using Fitweb.Application.Interfaces.Identity;
using Fitweb.Application.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Fitweb.Infrastructure.Identity.Settings;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Application.Interfaces;

namespace Fitweb.Infrastructure.Identity.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDateTimeProvider _dateTimeProvider;

        public JwtHandler(JwtSettings jwtSettings, IDateTimeProvider dateTimeProvider)
        {
            _jwtSettings = jwtSettings;
            _dateTimeProvider = dateTimeProvider;
        }

        public AuthDto Create(string userId, string username, IList<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keyBytes = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Issuer = _jwtSettings.Issuer,
                Expires = _dateTimeProvider.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256),
                NotBefore = _dateTimeProvider.Now,
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = new AuthDto
            {
                Token = tokenHandler.WriteToken(securityToken),
                Issuer = securityToken.Issuer,
                Subject = username,
                ValidFrom = securityToken.ValidFrom,
                ValidTo = securityToken.ValidTo
            };

            return token;
        }
    }
}
