using Fitweb.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Interfaces.Identity
{
    public interface IIdentityService
    {
        Task CreateUserAsync(string username, string email, string password);

        Task<AuthDto> LoginAsync(string username, string password);

        Task ConfirmEmailAsync(string code, string email);

        Task ResendEmailConfirmationAsync(string email);

        Task SendForgotPasswordEmailAsync(string email);

        Task ResetPasswordAsync(string email, string code, string newPassword);
    }
}
