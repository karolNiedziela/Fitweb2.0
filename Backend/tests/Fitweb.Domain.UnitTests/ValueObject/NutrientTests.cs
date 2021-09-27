using Fitweb.Domain.Exceptions;
using Fitweb.Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Domain.UnitTests.ValueObject
{
    public class NutrientTests
    {
        [Theory]
        [InlineData(10, 10, 5, null, null, null, null)]
        [InlineData(10, 10, 5, 5, null, null, null)]
        [InlineData(10, 10, 5, 5, 3, null, null)]
        [InlineData(10, 10, 5, 5, 3, 2, null)]
        [InlineData(10, 10, 5, 5, 3, 2, 4)]
        public void Create_ShouldPass_WhenParametersAreValid(double protein, double carbohydrate, double fat,
            double? saturatedFat, double? sugar, double? fiber, double? salt)
        {
            var nutrient = Nutrient.Create(protein, carbohydrate, fat, saturatedFat, sugar, fiber, salt);

            nutrient.Protein.Should().Be(protein);
            nutrient.Carbohydrate.Should().Be(carbohydrate);
            nutrient.Fat.Should().Be(fat);
            nutrient.SaturatedFat.Should().Be(saturatedFat);
            nutrient.Sugar.Should().Be(sugar);
            nutrient.Fiber.Should().Be(fiber);
            nutrient.Salt.Should().Be(salt);
        }


        [Fact]
        public void Create_ShouldThrowException_WhenProteinIsNegative()
        {
            var exception = Record.Exception(() => Nutrient.Create(-5, 10, 10));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeNumberException>();
            exception.Message.Should().Be("Protein cannot be negative.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenCarbohydrateIsNegative()
        {
            var exception = Record.Exception(() => Nutrient.Create(2, -3, 10));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeNumberException>();
            exception.Message.Should().Be("Carbohydrate cannot be negative.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenFatIsNegative()
        {
            var exception = Record.Exception(() => Nutrient.Create(15, 10, -100));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeNumberException>();
            exception.Message.Should().Be("Fat cannot be negative.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenSaturatedFatIsNegative()
        {
            var exception = Record.Exception(() => Nutrient.Create(5, 10, 10, -10));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeNumberException>();
            exception.Message.Should().Be("Saturated fat cannot be negative.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenSugarIsNegative()
        {
            var exception = Record.Exception(() => Nutrient.Create(5, 10, 10, 15, -150));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeNumberException>();
            exception.Message.Should().Be("Sugar cannot be negative.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenFiberIsNegative()
        {
            var exception = Record.Exception(() => Nutrient.Create(5, 10, 10, 15, 0, -2));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeNumberException>();
            exception.Message.Should().Be("Fiber cannot be negative.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenSaltIsNegative()
        {
            var exception = Record.Exception(() => Nutrient.Create(5, 10, 10, 15, 0, 0, -5));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeNumberException>();
            exception.Message.Should().Be("Salt cannot be negative.");
        }
    }
}
