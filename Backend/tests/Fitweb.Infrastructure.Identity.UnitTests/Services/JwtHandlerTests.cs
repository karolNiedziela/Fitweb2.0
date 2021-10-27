using Fitweb.Application.Constants;
using Fitweb.Application.DTO;
using Fitweb.Application.Interfaces;
using Fitweb.Application.Interfaces.Identity;
using Fitweb.Infrastructure.Identity.Services;
using Fitweb.Infrastructure.Identity.Settings;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Infrastructure.Identity.UnitTests.cs.Services
{
    public class JwtHandlerTests
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IJwtHandler _sut;
        private readonly IDateTimeProvider _dateTimeProvider;

        public JwtHandlerTests() 
        {
            _jwtSettings = new JwtSettings
            {
                Issuer = "testIssuer",
                ValidateIssuer = true,
                ValidateAudience = true,
                Audience = "testAudience",
                ExpiryMinutes = 10,
                Secret = "unitTestJwtSecretKey1234567"
            };

            _dateTimeProvider = Substitute.For<IDateTimeProvider>();

            _sut = new JwtHandler(_jwtSettings,_dateTimeProvider);
        }

        [Fact]
        public void Create_ShouldReturnAuthDto()
        {
            var userId = "testUserId";
            var username = "testUser";
            var roles = new List<string>
            {
                Roles.Athlete
            };

            _dateTimeProvider.Now.Returns(new DateTime(2020, 10, 10, 10, 10, 10, DateTimeKind.Utc));

            var authDto = _sut.Create(userId, username, roles);

            authDto.Should().BeOfType<AuthDto>();
            authDto.Issuer.Should().Be(_jwtSettings.Issuer);
            authDto.Subject.Should().Be(username);
            authDto.ValidTo.Should().BeCloseTo(new DateTime(2020, 10, 10, 10, 10, 10, DateTimeKind.Utc).AddMinutes(_jwtSettings.ExpiryMinutes),
                TimeSpan.FromSeconds(30));
        }
    }
}
