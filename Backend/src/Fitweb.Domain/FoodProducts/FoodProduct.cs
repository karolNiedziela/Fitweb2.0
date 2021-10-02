using Fitweb.Domain.Athletes;
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

        public string UserId { get; set; } = null;

        protected FoodProduct()
        {

        }

        public FoodProduct(Information information, Calories calories, Nutrient nutrient, FoodGroup? foodGroup, 
            string userId = null)
        {
            Information = information;
            Calories = calories;
            Nutrient = nutrient;
            Group = foodGroup;
            UserId = userId;
        }

        public void Update(FoodProduct foodProduct)
        {
            Information = Information.Update(foodProduct.Information);
            Calories = Calories.Update(foodProduct.Calories);
            Nutrient = Nutrient.Update(foodProduct.Nutrient);
            Group = foodProduct.Group;
        }
    }
}
