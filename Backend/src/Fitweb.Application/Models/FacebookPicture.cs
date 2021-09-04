using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Models
{
    public class FacebookPicture
    {
        [JsonProperty("data")]
        public FacebookPictureData FacebookPictureData { get; set; }
    }
}
