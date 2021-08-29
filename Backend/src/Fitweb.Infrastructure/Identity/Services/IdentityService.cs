using Fitweb.Application.DTO;
using Fitweb.Application.Exceptions;
using Fitweb.Application.Interfaces.Identity;
using Fitweb.Application.Settings;
using Fitweb.Infrastructure.Email;
using Fitweb.Infrastructure.Identity.Constants;
using Fitweb.Infrastructure.Identity.Entities;
using Fitweb.Infrastructure.Identity.Exceptions;
using Fitweb.Infrastructure.Identity.Extensions;
using Fitweb.Infrastructure.Identity.Factories;
using Fitweb.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenFactory _refreshTokenFactory;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly GeneralSettings _generalSettings;
        private readonly IEmailSender _emailSender;

        public IdentityService(UserManager<User> userManager, IJwtHandler jwtHandler, IRefreshTokenFactory refreshTokenFactory,
            IRefreshTokenRepository refreshTokenRepository, GeneralSettings generalSettings, IEmailSender emailSender)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _refreshTokenFactory = refreshTokenFactory;
            _refreshTokenRepository = refreshTokenRepository;
            _generalSettings = generalSettings;
            _emailSender = emailSender;
        }

        public async Task CreateUserAsync(string username, string email, string password)
        {
            var user = new User
            {
                UserName = username,
                Email = email,
            };

            //TODO: To remove, kept for testing purpose
            if (!_userManager.Options.SignIn.RequireConfirmedEmail)
            {
                user.EmailConfirmed = true;
            }

            var result = await _userManager.CreateAsync(user, password);

            await _userManager.AddToRoleAsync(user, Roles.Athlete);

            if (!result.Succeeded)
            {
                throw new IdentityException(result);
            }

            if (_userManager.Options.SignIn.RequireConfirmedEmail)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var code = Encode(token);
                var confirmationLink = CreateLink(_generalSettings.EmailConfirmation, user.Email, code);
                var emailMessage = new EmailMessage(new List<string>{user.Email }, LinkValues.EmailConfirmationLink, confirmationLink,
                    LinkValues.EmailConfirmationLinkText, null);

                await _emailSender.SendIdentityEmailAsync(emailMessage);
            }
        }

        public async Task<AuthDto> LoginAsync(string username, string password)
        {
            var user = await _userManager.FindByNameOrFailAsync(username);

            var isPasswordValid = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (isPasswordValid is PasswordVerificationResult.Failed)
            {
                throw new InvalidCredentialsException();
            }

            if (_userManager.Options.SignIn.RequireConfirmedEmail && !user.EmailConfirmed)
            {
                throw new EmailNotConfirmedException();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _jwtHandler.Create(user.Id, username, userRoles);

            var refreshToken = _refreshTokenFactory.Create(username);
            token.RefreshToken = refreshToken.Token;

            await _refreshTokenRepository.AddAsync(refreshToken);

            return token;
        }

        public async Task ResendEmailConfirmationAsync(string email)
        {
            var user = await _userManager.FindByEmailOrFailAsync(email);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var code = Encode(token);

            var confirmationLink = CreateLink(_generalSettings.EmailConfirmation, user.Email, code);
            var emailMessage = new EmailMessage(new List<string> { user.Email }, LinkValues.EmailConfirmationLink, confirmationLink,
                LinkValues.EmailConfirmationLinkText, null);

            await _emailSender.SendIdentityEmailAsync(emailMessage);
        }

        public async Task ConfirmEmailAsync(string code, string email)
        {
            var user = await _userManager.FindByEmailOrFailAsync(email);

            var token = Decode(code);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new IdentityException(result);
            }
        }

        public async Task SendForgotPasswordEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailOrFailAsync(email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var code = Encode(token);
            var forgotPasswordLink = CreateLink(_generalSettings.ForgotPassword, user.Email, code);

            var emailMessage = new EmailMessage(new List<string> { user.Email }, LinkValues.ForgotPasswordLink, forgotPasswordLink,
                LinkValues.ForgotPasswordLinkText, null);
            await _emailSender.SendIdentityEmailAsync(emailMessage);
        }

        public async Task ResetPasswordAsync(string email, string code, string newPassword)
        {
            var user = await _userManager.FindByEmailOrFailAsync(email);

            var token = Decode(code);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                throw new IdentityException(result);
            }
        }

        private string CreateLink(string destinationUrlPart, string email, string code)
        {
            // TODO: Change link when added client
            var link = string.Format(_generalSettings.AppDomain + destinationUrlPart, email, code);

            return link;
        }

        private static string Encode(string token)
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }

        private static string Decode(string code)
        {
            return Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }
    }
}


