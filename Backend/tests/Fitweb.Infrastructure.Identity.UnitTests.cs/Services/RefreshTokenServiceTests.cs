using Fitweb.Application.DTO;
using Fitweb.Application.Exceptions;
using Fitweb.Application.Interfaces;
using Fitweb.Application.Interfaces.Identity;
using Fitweb.Application.UnitTests.Fakes;
using Fitweb.Infrastructure.Identity.Constants;
using Fitweb.Infrastructure.Identity.Entities;
using Fitweb.Infrastructure.Identity.Exceptions;
using Fitweb.Infrastructure.Identity.Factories;
using Fitweb.Infrastructure.Identity.Services;
using Fitweb.Infrastructure.Persistence.Repositories;
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
    public class RefreshTokenServiceTests
    {
        private readonly IRefreshTokenFactory _refreshTokenFactory;
        private readonly FakeUserManager _fakeUserManager;
        private readonly IJwtHandler _jwtHandler;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRefreshTokenService _sut;

        public RefreshTokenServiceTests()
        {
            _refreshTokenFactory = Substitute.For<IRefreshTokenFactory>();
            _fakeUserManager = Substitute.For<FakeUserManager>();
            _jwtHandler = Substitute.For<IJwtHandler>();
            _dateTimeProvider = Substitute.For<IDateTimeProvider>();
            _refreshTokenRepository = Substitute.For<IRefreshTokenRepository>();

            _sut = new RefreshTokenService(_refreshTokenFactory, _fakeUserManager, _jwtHandler,
                _dateTimeProvider, _refreshTokenRepository);

        }

        [Fact]
        public async Task UseAsync_ShouldReturnNewAuthDto()
        {
            var refreshToken = new RefreshToken
            {
                CreatedAt = DateTime.UtcNow,
                RevokedAt = null,
                Token = "testRefreshToken",
                Username = "testUser"
            };

            var user = new User
            {
                UserName = "testUser"
            };

            var roles = new List<string>
            {
                Roles.Athlete
            };

            var jwt = new AuthDto
            {
                Issuer = "testIssuer",
                Token = "testToken",
            };

            var newRefreshToken = new RefreshToken
            {
                Token = "testNewRefreshToken",
                Username = "testUser"
            };

            _refreshTokenRepository.GetAsync(Arg.Any<string>()).Returns(refreshToken);

            _fakeUserManager.FindByNameAsync(Arg.Any<string>()).Returns(user);

            _fakeUserManager.GetRolesAsync(Arg.Any<User>()).Returns(roles);

            _jwtHandler.Create(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<List<string>>()).Returns(jwt);

            _refreshTokenFactory.Create(Arg.Any<string>()).Returns(newRefreshToken);


            await _sut.UseAsync(refreshToken.Token);

            refreshToken.IsRevoked.Should().BeTrue();
            newRefreshToken.Username.Should().Be(user.UserName);
            newRefreshToken.IsRevoked.Should().BeFalse();

            await _refreshTokenRepository.Received(1).AddAsync(Arg.Is<RefreshToken>(rf => rf.Username == user.UserName));
            await _refreshTokenRepository.Received(1).UpdateAsync(Arg.Is<RefreshToken>(rf => rf.IsRevoked == true));
        }

        [Fact]
        public async Task UseAsync_ShouldThrowInvalidRefreshTokenException_WhenTokenDoesNotExist()
        {
            var exception = await Record.ExceptionAsync(() => _sut.UseAsync("notExistingToken"));

            exception.Should().BeOfType<InvalidRefreshTokenException>();
            exception.Message.Should().Be("Refresh token was invalid.");
            ((AppException)exception).ErrorCode.Should().Be("invalid_refresh_token");
        }

        [Fact]
        public async Task RevokeAsync_ShouldRevokeToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = "refreshToken",
                Username = "testUser",
                RevokedAt = null
            };

            _refreshTokenRepository.GetAsync(Arg.Any<string>()).Returns(refreshToken);

            await _sut.RevokeAsync(refreshToken.Token);

            refreshToken.IsRevoked.Should().BeTrue();
            await _refreshTokenRepository.Received(1).UpdateAsync(Arg.Is<RefreshToken>(rf =>
                rf.Username == refreshToken.Username));
        }

        [Fact]
        public async Task RevokeAsync_ShouldThrowInvalidRefreshTokenException_WhenTokenDoesNotExist()
        {
            var exception = await Record.ExceptionAsync(() => _sut.RevokeAsync("notExistingToken"));

            exception.Should().BeOfType<InvalidRefreshTokenException>();
            exception.Message.Should().Be("Refresh token was invalid.");
            ((AppException)exception).ErrorCode.Should().Be("invalid_refresh_token");
        }
    }
}
