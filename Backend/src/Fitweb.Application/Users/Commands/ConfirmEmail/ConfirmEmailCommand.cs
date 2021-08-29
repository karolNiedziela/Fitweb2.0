using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Users.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest
    {
        public string Code { get; set; }

        public string Email { get; set; }
    }
}
