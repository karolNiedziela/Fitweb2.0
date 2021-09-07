using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Utilities.Csv.Models
{
    public class FoodProductCsv
    {
        public string Name { get; set; }

        public double? Calories { get; set; }

        public double? Fat { get; set; }

        public double? Protein { get; set; }

        public double? Carbohydrate { get; set; }

        public int? Group { get; set; }
    }
}
