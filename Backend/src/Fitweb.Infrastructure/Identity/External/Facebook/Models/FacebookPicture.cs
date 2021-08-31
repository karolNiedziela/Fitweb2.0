using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.External.Facebook.Models
{
    public class FacebookPicture
    {
        [JsonProperty("data")]
        public FacebookPictureData FacebookPictureData { get; set; }
    }
}
