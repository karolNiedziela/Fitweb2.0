using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Users.ResendConfirmationEmail
{
    public class ResendConfirmationEmailCommand : IRequest
    {
        public string Email { get; set; }
    }
}
