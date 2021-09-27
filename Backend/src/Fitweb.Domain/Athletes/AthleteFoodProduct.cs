using Fitweb.Domain.Common;
using Fitweb.Domain.FoodProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Athletes
{
    public class AthleteFoodProduct : Entity
    {
        public int AthleteId { get; private set; }

        public Athlete Athlete { get; private set; }

        public int FoodProductId { get; private set; }

        public FoodProduct FoodProduct { get; private set; }

        public double Weight { get; set; }

        protected AthleteFoodProduct()
        {

        }

        public AthleteFoodProduct(Athlete athlete, FoodProduct foodProduct, double weight)
        {
            Athlete = athlete;
            FoodProduct = foodProduct;
            Weight = DomainValidator.AgainstNegativeAndZeroNumber(weight, nameof(Weight));
        }
    }
}
