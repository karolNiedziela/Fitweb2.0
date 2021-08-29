using Fitweb.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Interfaces.Identity
{
    public interface IRefreshTokenService
    {
        Task RevokeAsync(string refreshToken);

        Task<AuthDto> UseAsync(string refreshToken);
    }
}
