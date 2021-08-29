using Fitweb.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Users.Commands.Login
{
    public class LoginCommand : IRequest<AuthDto>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
