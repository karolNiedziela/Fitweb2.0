using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.UnitTests.Builders
{
    public class FoodProductBuilder
    {
        public const string DefaultName = "test_product";

        public static FoodProduct Build(int id = 1, string name = DefaultName, string description = "test_description",
            double calories = 100, double protein = 10, double carbohydrate = 15, double fat = 15, double? sugar = null, 
            double? salt = null, double? fiber = null, double? saturatedFat = null, FoodGroup foodGroup = FoodGroup.Cereals) 
        {
            return new FoodProduct(Information.Create(name, description), Calories.Create(calories),
                Nutrient.Create(protein, carbohydrate, fat, saturatedFat, sugar, fiber, salt), foodGroup)
            {
                Id = id
            };
        }
    }
}
