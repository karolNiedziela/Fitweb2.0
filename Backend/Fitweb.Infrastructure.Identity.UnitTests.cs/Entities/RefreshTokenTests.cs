using Fitweb.Application.Exceptions;
using Fitweb.Infrastructure.Identity.Entities;
using Fitweb.Infrastructure.Identity.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Infrastructure.Identity.UnitTests.cs.Entities
{
    public class RefreshTokenTests
    {
        [Fact]
        public void ConstructorWithParameters_ShouldCreateNewRefreshTokenObject()
        {
            var token = "refreshToken";
            var username = "testUser";
            var createdAt = new DateTime(2012, 5, 10);

            var refreshToken = new RefreshToken(username, token, createdAt, null);

            refreshToken.Token.Should().Be(token);
            refreshToken.Username.Should().Be(username);
            refreshToken.CreatedAt.Should().Be(createdAt);
            refreshToken.RevokedAt.Should().BeNull();
        }

        [Fact]
        public void ConstructorWithParameters_ShouldThrowInvalidRefreshTokenException_WhenTokenIsNull()
        {
            var exception = Record.Exception(() => new RefreshToken("testUser", null, DateTime.UtcNow, null));

            exception.Should().BeOfType<InvalidRefreshTokenException>();
            exception.Message.Should().Be("Refresh token was invalid.");
            ((AppException)exception).ErrorCode.Should().Be("invalid_refresh_token");
        }

        [Fact]
        public void ConstructorWithParameters_ShouldThrowInvalidRefreshTokenException_WhenTokenIsNWhiteSpace()
        {
            var exception = Record.Exception(() => new RefreshToken("testUser", "  ", DateTime.UtcNow, null));

            exception.Should().BeOfType<InvalidRefreshTokenException>();
            exception.Message.Should().Be("Refresh token was invalid.");
            ((AppException)exception).ErrorCode.Should().Be("invalid_refresh_token");
        }

        [Fact]
        public void Use_ShouldSetRevokedAtToProvidedDate()
        {
            var refreshToken = new RefreshToken
            {
                Token = "refreshToken",
                RevokedAt = null
            };

            var usedAt = new DateTime(2021, 12, 10);
            refreshToken.Use(usedAt);

            refreshToken.RevokedAt.Should().Be(usedAt);
            refreshToken.IsRevoked.Should().BeTrue();
        }

        [Fact]
        public void Use_ShouldThrowRevokedRefreshTokenException_WhenTokenIsAlreadyRevoked()
        {
            var revokedAt = new DateTime(2021, 12, 10);
            var refreshToken = new RefreshToken
            {
                Token = "refreshToken",
                RevokedAt = revokedAt
            };

            var exception = Record.Exception(() => refreshToken.Use(DateTime.UtcNow));

            exception.Should().BeOfType<RevokedRefreshTokenException>();
            exception.Message.Should().Be("Refresh token has been revoked.");
            ((AppException)exception).ErrorCode.Should().Be("revoked_refresh_token");
        }
    }
}
