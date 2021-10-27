using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Fitweb.Application.UnitTests")]
namespace Fitweb.Application.Requests
{

    public class AuthorizeRequest 
    {
        internal string UserId { get; set; }

        internal bool IsAdmin { get; set; } = false;

    }
}
