using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.DTO
{
    public class FoodProductDto
    {
        [JsonProperty(Order = 1)]
        public int Id { get; set; }

        [JsonProperty(Order = 2)]

        public string Name { get; set; }

        [JsonProperty(Order = 4)]
        public double Calories { get; set; }

        [JsonProperty(Order = 4)]
        public double Protein { get; set; }

        [JsonProperty(Order = 4)]
        public double Carbohydrate { get; set; }

        [JsonProperty(Order = 4)]
        public double Fat { get; set; }

        [JsonProperty(Order = 6)]
        public string Group { get; set; }
    }
}
