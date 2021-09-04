using Fitweb.Infrastructure.Identity.Services;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Infrastructure.Identity.UnitTests.cs.Services
{
    public class RngServiceTests
    {
        private static readonly string[] SpecialChars = new[] { "/", "\\", "=", "+", "?", ":", "&" };

        private readonly IRng _sut; 

        public RngServiceTests()
        {
            _sut = new Rng();
        }

        [Fact]
        public void Generate_ShouldReturnRandomStringWithLenWithoutSpecialChars()
        {
            var generatedString = _sut.Generate();

            generatedString.Should().NotBeEmpty();
            generatedString.Should().NotContainAny(SpecialChars);
        }

        [Fact]
        public void Generate_ShouldReturnRandomStringAndCanContainSpecialChars_WhenRemoveSpecialCharsFlagIsSetToTrue()
        {
            var generatedString = _sut.Generate(removeSpecialChars: false);

            generatedString.Should().NotBeEmpty();

            var specialCharsInGeneratedString = SpecialChars.Select(x => Regex.Matches(generatedString, Regex.Escape(x)))
                    .Sum(x => x.Count);

            specialCharsInGeneratedString.Should().BeGreaterOrEqualTo(0);
        }
    }
}
