using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Exceptions
{
    public class IdentityException : Exception
    {
        public IdentityResult Error { get; set; }

        public IdentityException(IdentityResult error)
        {
            Error = error;
        }
    }
}
