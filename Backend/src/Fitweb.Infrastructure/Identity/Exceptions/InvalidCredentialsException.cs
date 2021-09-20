using Fitweb.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Exceptions
{
    public class InvalidCredentialsException : AppException
    {
        public override string ErrorCode => "invalid_credentials";

        public InvalidCredentialsException() : base("Invalid credentials.")
        {

        }
    }
}
