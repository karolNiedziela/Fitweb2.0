using Fitweb.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Exceptions
{
    public class EmailSendingException : AppException
    {
        public override string ErrorCode => "email_sending";

        public EmailSendingException(string message) : base(message)
        {

        }
    }
}
