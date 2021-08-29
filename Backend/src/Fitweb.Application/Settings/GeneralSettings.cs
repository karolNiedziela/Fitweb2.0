using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Settings
{
    public class GeneralSettings
    {
        public const string General = "General";

        public string Name { get; set; }

        public string AppDomain { get; set; }

        public string EmailConfirmation { get; set; }

        public string ClientAppUrl { get; set; }

        public string ForgotPassword { get; set; }
    }
}
