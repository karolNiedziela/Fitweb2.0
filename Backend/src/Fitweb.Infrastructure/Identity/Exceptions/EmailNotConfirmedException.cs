using Fitweb.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Exceptions
{
    public class EmailNotConfirmedException : AppException
    {
        public override string ErrorCode => "email_not_confirmed";

        public EmailNotConfirmedException() : base("Email not confirmed. Confirm email to login.")
        {

        }
    }
}
