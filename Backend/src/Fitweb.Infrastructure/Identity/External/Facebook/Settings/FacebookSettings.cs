using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.External.Facebook.Settings
{
    public class FacebookSettings
    {
        public const string Facebook = "Facebook";

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string[] Fields { get; set; }
    }
}
