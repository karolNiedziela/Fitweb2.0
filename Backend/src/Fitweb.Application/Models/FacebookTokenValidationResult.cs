using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Models
{
    public class FacebookTokenValidationResult
    {
        [JsonProperty("data")]

        public FacebookTokenValidationData FacebookTokenValidationData { get; set; }
    }
}
