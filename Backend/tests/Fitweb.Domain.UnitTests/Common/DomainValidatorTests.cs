using Fitweb.Domain.Common;
using Fitweb.Domain.Exceptions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Domain.UnitTests.Common
{
    public class DomainValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AgainstEmptyString_ShouldThrowExceptionWithDefaultName_WhenStringIsEmptyOrNull(string value)
        {
            var exception = Record.Exception(() => DomainValidator.AgainstEmptyString(value));

            exception.Should().NotBeNull();
            exception.Message.Should().Be("Value cannot be null or empty.");
            exception.Should().BeOfType<EmptyStringException>();
        }

        [Theory]
        [InlineData(null, "test")]
        [InlineData("", "someValue")]
        public void AgainstEmptyString_ShouldThrowExceptionWitGivenValue_WhenStringIsEmptyOrNull(string value, string name)
        {
            var exception = Record.Exception(() => DomainValidator.AgainstEmptyString(value, name));

            exception.Should().NotBeNull();
            exception.Message.Should().Be($"{name} cannot be null or empty.");
            exception.Should().BeOfType<EmptyStringException>();
        }

        [Fact]
        public void AgainstEmptyString_ShouldPass_WhenStringValueIsProper()
        {
            var value = new string('*', 5);

            var exception = Record.Exception(() => DomainValidator.AgainstEmptyString(value));

            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(-5d)]
        [InlineData(-0.1d)]
        public void AgainstNegativeNumber_ShouldThrowException_WhenValueIsLowerThanOrEqualToZero(double value)
        {
            var exception = Record.Exception(() => DomainValidator.AgainstNegativeNumber(value));

            exception.Should().NotBeNull();
            exception.Message.Should().Be($"Value cannot be negative.");
            exception.Should().BeOfType<NegativeNumberException>();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(0d)]
        public void AgainstNegativeNumber_ShouldPass_WhenValueIsGreaterZero(double value)
        {
            var exception = Record.Exception(() => DomainValidator.AgainstNegativeNumber(value));

            exception.Should().BeNull();
        }

        [Fact]
        public void AgainstNegativeNumberNullableValue_ShouldThrowException_WhenValueIsLowerThanOrEqualToZero()
        {
            double? value = -5;

            var exception = Record.Exception(() => DomainValidator.AgainstNegativeNumber(value));

            exception.Should().NotBeNull();
            exception.Message.Should().Be($"Value cannot be negative.");
            exception.Should().BeOfType<NegativeNumberException>();
        }

        [Fact]
        public void AgainstNegativeNumberNullableValue_ShouldPass_WhenValueIsNull()
        {
            double? value = null;

            var exception = Record.Exception(() => DomainValidator.AgainstNegativeNumber(value));

            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        public void AgainstNegativeNumberNullableValue_ShouldPass_WhenValueIsPositiveOrZero(double? value)
        {
            var exception = Record.Exception(() => DomainValidator.AgainstNegativeNumber(value));

            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(-5d)]
        [InlineData(0d)]
        public void AgainstNegativeAndZeroNumber_ShouldThrowException_WhenValueIsLowerOrEqualToZero(double value)
        {
            var exception = Record.Exception(() => DomainValidator.AgainstNegativeAndZeroNumber(value));

            exception.Should().NotBeNull();
            exception.Message.Should().Be($"Value cannot be negative or zero.");
            exception.Should().BeOfType<NegativeOrZeroNumberException>();
        }

        [Fact]
        public void AgainstNegativeAndZeroNumber_ShouldPass_WhenValueIsPositive()
        { 
            double value = 10;

            var exception = Record.Exception(() => DomainValidator.AgainstNegativeAndZeroNumber(value));

            exception.Should().BeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-3)]
        public void AgainstNegativeAndZeroNumberNullable_ShouldThrowException_WhenValueIsNegativeOrZero(double? value)
        {
            var exception = Record.Exception(() => DomainValidator.AgainstNegativeAndZeroNumber(value));

            exception.Should().NotBeNull();
            exception.Message.Should().Be("Value cannot be negative or zero.");
            exception.Should().BeOfType<NegativeOrZeroNumberException>();
        }

        [Fact]
        public void AgainstNegativeAndZeroNumberNullable_ShouldPass_WhenValueIsPositive()
        {
            double? value = 3;

            var exception = Record.Exception(() => DomainValidator.AgainstNegativeAndZeroNumber(value));

            exception.Should().BeNull();
        }

        [Fact]
        public void AgainstImproperPeriod_ShouldThrowException_WhenPeriodIsImproper()
        {
            var startDate = new DateTime(2021, 10, 10);
            var endDate = new DateTime(2021, 5, 5);

            var exception = Record.Exception(() => DomainValidator.AgainstImproperPeriod(startDate, endDate));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<ImproperPeriodException>();
            exception.Message.Should().Be("Incorrect time interval.");
        }


    }
}
