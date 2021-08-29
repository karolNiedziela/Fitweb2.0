using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.DTO
{
    public class AuthDto
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string Issuer { get; set; }

        public string Subject { get; set; } // TODO: Verify it

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
