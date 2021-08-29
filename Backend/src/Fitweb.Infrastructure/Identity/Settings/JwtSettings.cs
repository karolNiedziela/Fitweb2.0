using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Settings
{
    public class JwtSettings
    {
        public const string Jwt = "Jwt";

        public string Secret { get; set; }

        public int ExpiryMinutes { get; set; }

        public bool ValidateIssuer { get; set; }

        public string Issuer { get; set; }

        public bool ValidateAudience { get; set; }

        public string Audience { get; set; }
    }
}
