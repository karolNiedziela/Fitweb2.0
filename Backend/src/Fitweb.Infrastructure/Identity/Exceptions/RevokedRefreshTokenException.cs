using Fitweb.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Exceptions
{
    public class RevokedRefreshTokenException : AppException
    {
        public override string ErrorCode => throw new NotImplementedException();

        public RevokedRefreshTokenException() : base("Refresh token has been revoked.")
        {

        }
    }
}
