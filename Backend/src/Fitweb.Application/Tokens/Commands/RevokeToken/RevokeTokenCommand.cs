using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Tokens.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest
    {
        public string RefreshToken { get; set; }
    }
}
