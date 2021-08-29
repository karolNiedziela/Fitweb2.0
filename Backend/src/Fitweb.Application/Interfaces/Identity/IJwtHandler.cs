using Fitweb.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Interfaces.Identity
{
    public interface IJwtHandler
    {
        AuthDto Create(string userId, string username, IList<string> roles);
    }
}
