using Fitweb.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Interfaces.Identity
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);

        Task<FacebookUserInformationResult> GetUserInformationAsync(string accessToken);
    }
}
