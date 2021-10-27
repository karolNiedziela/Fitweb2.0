using Fitweb.Application.Constants;
using Fitweb.Application.DTO;
using Fitweb.Application.Exceptions;
using Fitweb.Application.Interfaces.Identity;
using Fitweb.Application.Interfaces.Utilities.Email;
using Fitweb.Application.Models;
using Fitweb.Application.Settings;
using Fitweb.Application.UnitTests.Fakes;
using Fitweb.Domain.Exceptions;
using Fitweb.Infrastructure.Identity.Entities;
using Fitweb.Infrastructure.Identity.Exceptions;
using Fitweb.Infrastructure.Identity.Extensions;
using Fitweb.Infrastructure.Identity.Factories;
using Fitweb.Infrastructure.Identity.Services;
using Fitweb.Infrastructure.Persistence.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MimeKit;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Infrastructure.Identity.UnitTests.cs.Services
{
    public class IdentityServiceTests
    {
        private readonly FakeUserManager _fakeUserManager;
        private readonly IIdentityService _sut;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenFactory _refreshTokenFactory;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly GeneralSettings _generalSettings;
        private readonly IEmailSender _emailSender;
        private readonly IFacebookAuthService _facebookAuthService;

        public IdentityServiceTests()
        {
            _fakeUserManager = Substitute.For<FakeUserManager>();
            _jwtHandler = Substitute.For<IJwtHandler>();
            _refreshTokenFactory = Substitute.For<IRefreshTokenFactory>();
            _refreshTokenRepository = Substitute.For<IRefreshTokenRepository>();
            _generalSettings = Substitute.For<GeneralSettings>();
            _emailSender = Substitute.For<IEmailSender>();
            _facebookAuthService = Substitute.For<IFacebookAuthService>();

            _sut = new IdentityService(_fakeUserManager, _jwtHandler,
                _refreshTokenFactory, _refreshTokenRepository, _generalSettings,
                _emailSender, _facebookAuthService);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldAddNewUser()
        {
            var userName = "testUser";
            var email = "testUser@email.com";
            var password = "test";

            _fakeUserManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Success);

            await _sut.CreateUserAsync(userName, email, password);

            await _fakeUserManager.Received(1).CreateAsync(Arg.Is<User>(x => x.UserName == userName &&
                x.Email == email), Arg.Is<string>(x => x.Equals(password)));
        }

        [Fact]
        public async Task CreateUserAsync_ShouldAddNewUserAndSendEmail_WhenRequireConfirmedEmailIsSetToTrue()
        {
            var userName = "testUser";
            var email = "testUser@email.com";
            var password = "test";
            var emailToken = "asdasdasdsaddas";

            _fakeUserManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Success);

            _fakeUserManager.Options.SignIn.RequireConfirmedEmail = true;

            _fakeUserManager.GenerateEmailConfirmationTokenAsync(Arg.Any<User>())
                .Returns(emailToken);

            await _sut.CreateUserAsync(userName, email, password);

            await _fakeUserManager.Received(1).CreateAsync(Arg.Is<User>(x => x.UserName == userName &&
                x.Email == email), Arg.Is<string>(x => x.Equals(password)));

            await _fakeUserManager.Received(1).GenerateEmailConfirmationTokenAsync(Arg.Is<User>(x => x.UserName == userName &&
                x.Email == email));

            await _emailSender.Received(1).SendIdentityEmailAsync(Arg.Any<EmailMessage>());

        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowIdentityException_WhenUserNameIsDuplicated()
        {
            var userName = "testUser";
            var email = "testUser@email.com";
            var password = "test";

            _fakeUserManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(new IdentityErrorDescriber().DuplicateEmail(email)));

            var exception = await Record.ExceptionAsync(() => _sut.CreateUserAsync(userName, email, password));

            exception.Should().BeOfType<IdentityException>();
            ((IdentityException)exception).Error.Errors.FirstOrDefault().Code.Should().Be("DuplicateEmail");
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowIdentityException_WhenEmailIsDuplicated()
        {
            var userName = "testUser";
            var email = "testUser@email.com";
            var password = "test";

            _fakeUserManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(new IdentityErrorDescriber().DuplicateUserName(userName)));

            var exception = await Record.ExceptionAsync(() => _sut.CreateUserAsync(userName, email, password));

            exception.Should().BeOfType<IdentityException>();
            ((IdentityException)exception).Error.Errors.FirstOrDefault().Code.Should().Be("DuplicateUserName");
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowIdentityException_WhenPasswordIsShorterThanSixCharacters()
        {
            var userName = "testUser";
            var email = "testUser@email.com";
            var password = "test";

            _fakeUserManager.Options.Password.RequiredLength = 6;

            _fakeUserManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(new IdentityErrorDescriber().PasswordTooShort(password.Length)));
            

            var exception = await Record.ExceptionAsync(() => _sut.CreateUserAsync(userName, email, password));

            exception.Should().BeOfType<IdentityException>();
            ((IdentityException)exception).Error.Errors.FirstOrDefault().Code.Should().Be("PasswordTooShort");
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowIdentityException_WhenPasswordDoestNotContainLowercase()
        {
            var userName = "testUser";
            var email = "testUser@email.com";
            var password = "TEST";

            _fakeUserManager.Options.Password.RequiredLength = 6;

            _fakeUserManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(new IdentityErrorDescriber().PasswordRequiresLower()));


            var exception = await Record.ExceptionAsync(() => _sut.CreateUserAsync(userName, email, password));

            exception.Should().BeOfType<IdentityException>();
            ((IdentityException)exception).Error.Errors.FirstOrDefault().Code.Should().Be("PasswordRequiresLower");
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowIdentityException_WhenPasswordDoestNotContainUppercase()
        {
            var userName = "testUser";
            var email = "testUser@email.com";
            var password = "test";

            _fakeUserManager.Options.Password.RequiredLength = 6;

            _fakeUserManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(new IdentityErrorDescriber().PasswordRequiresUpper()));


            var exception = await Record.ExceptionAsync(() => _sut.CreateUserAsync(userName, email, password));

            exception.Should().BeOfType<IdentityException>();
            ((IdentityException)exception).Error.Errors.FirstOrDefault().Code.Should().Be("PasswordRequiresUpper");
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowIdentityException_WhenPasswordDoesNotContainOneUniqueChar()
        {
            var userName = "testUser";
            var email = "testUser@email.com";
            var password = "test";

            _fakeUserManager.Options.Password.RequiredLength = 6;

            _fakeUserManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(new IdentityErrorDescriber().PasswordRequiresUniqueChars(1)));


            var exception = await Record.ExceptionAsync(() => _sut.CreateUserAsync(userName, email, password));

            exception.Should().BeOfType<IdentityException>();
            ((IdentityException)exception).Error.Errors.FirstOrDefault().Code.Should().Be("PasswordRequiresUniqueChars");
        }

        [Fact]
        public async Task CreateUserAsync_ShouldThrowIdentityException_WhenPasswordDoesNotDigit()
        {
            var userName = "testUser";
            var email = "testUser@email.com";
            var password = "test";

            _fakeUserManager.Options.Password.RequiredLength = 6;

            _fakeUserManager.CreateAsync(Arg.Any<User>(), Arg.Any<string>())
                .Returns(IdentityResult.Failed(new IdentityErrorDescriber().PasswordRequiresDigit()));


            var exception = await Record.ExceptionAsync(() => _sut.CreateUserAsync(userName, email, password));

            exception.Should().BeOfType<IdentityException>();
            ((IdentityException)exception).Error.Errors.FirstOrDefault().Code.Should().Be("PasswordRequiresDigit");
        }

        [Fact]
        public async Task LoginAsync_ShouldFail_WhenUserWithGivenUsernameWasNotFound()
        {
            var userName = "testUser";
            var password = "test";

            _fakeUserManager.FindByNameAsync(Arg.Any<string>())
                .Returns((User)null);

            var exception = await Record.ExceptionAsync(() => _sut.LoginAsync(userName, password));

            exception.Should().BeOfType<InvalidCredentialsException>();
            exception.Message.Should().Be("Invalid credentials.");
            ((AppException)exception).ErrorCode.Should().Be("invalid_credentials");
        }

        [Fact]
        public async Task LoginAsync_ShouldFail_WhenPasswordDoesNotMatch()
        {
            var user = new User
            {
                UserName = "testUser",
            };

            var password = "testPass";

            user.PasswordHash = "randomString";

            _fakeUserManager.FindByNameAsync(Arg.Any<string>()).Returns(user);

            var exception = await Record.ExceptionAsync(() => _sut.LoginAsync(user.UserName, password));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidCredentialsException>();
            exception.Message.Should().Be("Invalid credentials.");
        }

        [Fact]
        public async Task LoginAsync_ShouldFail_WhenRequireConfirmedEmailIsSetToTrueAndUserDidNotConfirmEmail()
        {
            var user = new User
            {
                UserName = "testUser",
            };
            var password = "testPass";

            _fakeUserManager.FindByNameAsync(Arg.Any<string>()).Returns(user);

            _fakeUserManager.PasswordHasher.VerifyHashedPassword(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(PasswordVerificationResult.Success);

            _fakeUserManager.Options.SignIn.RequireConfirmedEmail = true;

            var exception = await Record.ExceptionAsync(() => _sut.LoginAsync(user.UserName, password));

            exception.Should().BeOfType<EmailNotConfirmedException>();
        }


        [Fact]
        public async Task LoginAsync_ShouldReturnAuthDto()
        {
            var user = new User
            {
                UserName = "testUser",
                EmailConfirmed = true
            };
            var password = "testPass";

            _fakeUserManager.FindByNameAsync(Arg.Any<string>()).Returns(user);

            _fakeUserManager.PasswordHasher.VerifyHashedPassword(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<string>())
                .Returns(PasswordVerificationResult.Success);

            _fakeUserManager.Options.SignIn.RequireConfirmedEmail = true;

            _fakeUserManager.GetRolesAsync(Arg.Any<User>())
                .Returns(new List<string> { Roles.Athlete });

            var authDto = new AuthDto
            {
                Issuer = "issuer",
                Subject = "subject",
                Token = "token",
                ValidFrom = new DateTime(2021, 10, 11),
                ValidTo = new DateTime(2021, 10, 13),
            };

            _jwtHandler.Create(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<IList<string>>())
                .Returns(authDto);

            var refreshToken = new RefreshToken
            {
                Token = "refreshToken"
            };
            authDto.RefreshToken = refreshToken.Token;
            _refreshTokenFactory.Create(Arg.Any<string>())
                .Returns(refreshToken);

            var token = await _sut.LoginAsync(user.UserName, password);

            token.Should().NotBeNull();
            token.Should().BeEquivalentTo(authDto);
        }

        [Fact]
        public async Task ResendEmailConfirmationAsync_ShouldSendEmail()
        {
            var user = new User
            {
                UserName = "testUser",
                Email = "testUser@email.com"
            };

            _fakeUserManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);

            var token = "testEmailToken";

            _fakeUserManager.GenerateEmailConfirmationTokenAsync(Arg.Any<User>()).Returns(token);

            await _sut.ResendEmailConfirmationAsync(user.Email);

            await _emailSender.Received(1).SendIdentityEmailAsync(Arg.Any<EmailMessage>());
        }

        [Fact]
        //TODO: Probably to repair
        public async Task ConfirmEmailAsync_ShouldConfirmUserEmail()
        {
            var user = new User
            {
                UserName = "testUser",
                Email = "testUser@email.com"
            };

            _fakeUserManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);

            _fakeUserManager.ConfirmEmailAsync(user, Arg.Any<string>()).Returns(IdentityResult.Success);

            await _sut.ConfirmEmailAsync("testCode", user.Email);
        }

        [Fact]
        public async Task ConfirmEmailAsync_ShouldThrowIdentityException_WhenTokenIsInvalid()
        {
            var user = new User
            {
                UserName = "testUser",
                Email = "testUser@email.com"
            };

            _fakeUserManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);

            _fakeUserManager.ConfirmEmailAsync(user, Arg.Any<string>())
                .Returns(IdentityResult.Failed(new IdentityErrorDescriber().InvalidToken()));

            var exception = await Record.ExceptionAsync(() => _sut.ConfirmEmailAsync("testCode", user.Email));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<IdentityException>();
            ((IdentityException)exception).Error.Errors.FirstOrDefault().Code.Should().Be("InvalidToken");
        }

        [Fact]
        public async Task SendForgotPasswordEmailAsync_ShouldSendForgotPasswordEmail()
        {
            var user = new User
            {
                UserName = "testUser",
                Email = "testUser@email.com"
            };

            _fakeUserManager.FindByEmailAsync(Arg.Any<string>()).Returns(user);

            var token = "testToken";
            _fakeUserManager.GeneratePasswordResetTokenAsync(Arg.Any<User>()).Returns(token);

            await _sut.SendForgotPasswordEmailAsync(user.Email);

            var to = new List<MailboxAddress> {
                MailboxAddress.Parse(user.Email)
            };
            await _emailSender.Received(1).SendIdentityEmailAsync(Arg.Is<EmailMessage>(x => 
            x.Attachments == null &&
            x.LinkText == LinkValues.ForgotPasswordLinkText));
        }
    }
}
