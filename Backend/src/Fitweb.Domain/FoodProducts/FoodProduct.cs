using Fitweb.Domain.Common;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.FoodProducts
{
    public class FoodProduct : Entity
    {
        public Information Information { get; private set; }

        public Calories Calories { get; private set; }

        public Nutrient Nutrient { get; private set; }

        public FoodGroup? Group { get; private set; }

        protected FoodProduct()
        {

        }

        public FoodProduct(Information information, Calories calories, Nutrient nutrient, FoodGroup? foodGroup)
        {
            Information = information;
            Calories = calories;
            Nutrient = nutrient;
            Group = foodGroup;
        }
    }
}
