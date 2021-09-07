using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.DTO
{
    public class FoodProductDetailsDto : FoodProductDto
    {
        [JsonProperty(Order = 3)]
        public string Description { get; set; }

        [JsonProperty(Order = 5)]
        public double? Sugar { get; set; }

        [JsonProperty(Order = 5)]
        public double? SaturatedFat { get; set; }

        [JsonProperty(Order = 5)]
        public double? Fiber { get; set; }

        [JsonProperty(Order = 5)]
        public double? Salt { get; set; }

    }
}
