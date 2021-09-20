using Fitweb.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Users.FacebookLogin
{
    public class FacebookLoginCommand : IRequest<AuthDto>
    {
        public string AccessToken { get; set; }
    }
}
