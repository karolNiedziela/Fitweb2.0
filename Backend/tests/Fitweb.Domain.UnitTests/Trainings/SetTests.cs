using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Trainings;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Domain.UnitTests.Trainings
{
    public class SetTests
    {
        [Fact]
        public void CallingSetConstructor_ShouldCreateSetObject_WhenParametersAreValid()
        {
            var set = new Set(20, 3);

            set.Weight.Should().Be(20);
            set.NumberOfReps.Should().Be(3);
            set.NumberOfSets.Should().Be(1);
        }

        [Theory]
        [InlineData(-10)]
        [InlineData(0)]
        public void CallingSetConstructor_ShouldThrowException_WhenWeightIsNegativeOrZero(double weight)
        {
            var exception = Record.Exception(() => new Set(weight, 3));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeOrZeroNumberException>();
            exception.Message.Should().Be("Weight cannot be negative or zero.");
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void CallingSetConstructor_ShouldThrowException_WhenNumberOfRepsAreNegativeOrZero(int numberOfReps)
        {
            var exception = Record.Exception(() => new Set(20, numberOfReps));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeOrZeroNumberException>();
            exception.Message.Should().Be("Number of reps cannot be negative or zero.");
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        public void CallingSetConstructor_ShouldThrowException_WhenNumberOfSetsAreNegativeOrZero(int numberOfSets)
        {
            var exception = Record.Exception(() => new Set(20, 2, numberOfSets));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NegativeOrZeroNumberException>();
            exception.Message.Should().Be("Number of sets cannot be negative or zero.");
        }    
    }
}
