using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Users.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public string Email { get; set; }

        public string Code { get; set; }

        public string NewPassword { get; set; }
    }
}
