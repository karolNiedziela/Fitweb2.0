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
    public class CaloriesTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        public void Create_ShouldPass_WhenValueIsPositiveOrZero(double value)
        {
            var calories = Calories.Create(value);

            calories.Value.Should().Be(value);
        }

        [Fact]
        public void Create_ShouldThrowException_WhenValueIsNegative()
        {
            var exception = Record.Exception(() => Calories.Create(-50));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeNumberException>();
            exception.Message.Should().Be("Calories cannot be negative.");
        }
    }
}
