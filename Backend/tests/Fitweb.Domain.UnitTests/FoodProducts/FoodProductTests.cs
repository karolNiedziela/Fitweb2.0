using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Domain.UnitTests.FoodProducts
{
    public class FoodProductTests
    {
        [Fact]
        public void CallingFoodProductConstructor_ShouldCreateNewFoodProductObject_WhenParametersAreValid()
        {
            var information = Information.Create("foodProduct", "foodProductDescription");
            var nutrient = Nutrient.Create(10, 20, 30);
            var calories = Calories.Create(100);

            var foodProduct = new FoodProduct(information, calories, nutrient, FoodGroup.Fruit);

            foodProduct.Information.Should().Equals(information);
            foodProduct.Nutrient.Should().Equals(nutrient);
            foodProduct.Calories.Should().Equals(calories);
            foodProduct.FoodGroup.Should().Be(FoodGroup.Fruit);
        }
    }
}
