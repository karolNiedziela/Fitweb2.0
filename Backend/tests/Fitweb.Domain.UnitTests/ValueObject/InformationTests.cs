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
    public class InformationTests
    {
        [Theory]
        [InlineData("test", null)]
        [InlineData("test123", "")]
        [InlineData("test1", "testDescription")]
        public void Create_ShouldPass_WhenParametersAreValid(string name, string description)
        {
            var information = Information.Create(name, description);

            information.Name.Should().Be(name);
            information.Description.Should().Be(description);
        }

        [Fact]
        public void Create_ShouldThrowException_WhenNameIsNull()
        {
            var exception = Record.Exception(() => Information.Create(null, null));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<EmptyStringException>();
            exception.Message.Should().Be("Name cannot be null or empty.");
        }

        [Fact]
        public void Create_ShouldThrowException_WhenNameIsEmpty()
        {
            var exception = Record.Exception(() => Information.Create("", null));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<EmptyStringException>();
            exception.Message.Should().Be("Name cannot be null or empty.");
        }
    }
}
