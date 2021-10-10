using Fitweb.Application.DTO;
using Fitweb.Application.Interfaces.Utilities.Email;
using Fitweb.Application.Interfaces.Identity;
using Fitweb.Application.Models;
using Fitweb.Application.Settings;
using Fitweb.Infrastructure.Identity.Constants;
using Fitweb.Infrastructure.Identity.Entities;
using Fitweb.Infrastructure.Identity.Exceptions;
using Fitweb.Infrastructure.Identity.Extensions;
using Fitweb.Infrastructure.Identity.Factories;
using Fitweb.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
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
        private readonly IFacebookAuthService _facebookAuthService;
        public IdentityService(UserManager<User> userManager, IJwtHandler jwtHandler, IRefreshTokenFactory refreshTokenFactory,
            IRefreshTokenRepository refreshTokenRepository, GeneralSettings generalSettings, IEmailSender emailSender, 
            IFacebookAuthService facebookAuthService)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _refreshTokenFactory = refreshTokenFactory;
            _refreshTokenRepository = refreshTokenRepository;
            _generalSettings = generalSettings;
            _emailSender = emailSender;
            _facebookAuthService = facebookAuthService;
        }

        public async Task<string> CreateUserAsync(string username, string email, string password)
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

            var createdUser = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(createdUser, Roles.Athlete);

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

            return user.Id;
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

        public async Task<AuthDto> FacebookLoginAsync(string accessToken)
        {
            var validatedTokenResult = await _facebookAuthService.ValidateAccessTokenAsync(accessToken);

            if (!validatedTokenResult.FacebookTokenValidationData.IsValid)
            {
                // TODO: Throw proper exception
                throw new Exception();
            }

            var userInformation = await _facebookAuthService.GetUserInformationAsync(accessToken);

            var token = await ExternalLoginAsync("Facebook", userInformation.Id, userInformation.Email);

            return token;
        }

        private string CreateLink(string destinationUrlPart, string email, string code)
        {
            // TODO: Change link when added client
            var link = string.Format(_generalSettings.AppDomain + destinationUrlPart, email, code);

            return link;
        }

        private async Task<AuthDto> ExternalLoginAsync(string loginProvider, string providerKey, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                var newUser = new User
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };


                var result = await _userManager.CreateAsync(newUser);
                if (!result.Succeeded)
                {
                    throw new IdentityException(result);
                }

                await _userManager.AddToRoleAsync(newUser, Roles.Athlete);

                var loginInfo = new UserLoginInfo(loginProvider, providerKey, email);

                var addLoginResult = await _userManager.AddLoginAsync(newUser, loginInfo);
                if (!addLoginResult.Succeeded)
                {
                    throw new IdentityException(addLoginResult);
                }

                user = newUser;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _jwtHandler.Create(user.Id, email, userRoles);

            var refreshToken = _refreshTokenFactory.Create(email);
            token.RefreshToken = refreshToken.Token;

            await _refreshTokenRepository.AddAsync(refreshToken);

            return token;
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


