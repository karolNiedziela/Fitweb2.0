using Fitweb.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Exceptions
{
    public class InvalidRefreshTokenException : AppException
    {
        public override string ErrorCode => "invalid_refresh_token";

        public InvalidRefreshTokenException() : base("Refresh token was invalid.")
        {

        }
    }
}
