using Fitweb.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Tokens.Commands.UseToken
{
    public class UseTokenCommand : IRequest<AuthDto>
    {
        public string RefreshToken { get; set; }
    }
}
