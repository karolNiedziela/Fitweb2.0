using Fitweb.Domain.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Domain.UnitTests.Extensions
{
    public class EnumExtensionsTests
    {
        [Theory]
        [InlineData(TestEnum.TestValue, "TestValue")]
        [InlineData(TestEnum.ThirdValue, "Third value")]
        public void GetDisplayName_ShouldReturnDisplayName_WhenEnumIsNotNull(TestEnum @enum, string exceptedResult)
        {
            var displayName = @enum.GetDisplayName();

            displayName.Should().Be(exceptedResult);
        }

        [Fact]
        public void GetDisplayName_ShouldEnumMember_WhenDisplayAttributeIsNotDefined()
        {
            var displayName = TestEnum.SecondValue.GetDisplayName();

            displayName.Should().Be("SecondValue");
        }

        public enum TestEnum
        {
            [Display(Name = "TestValue")]
            TestValue = 1,

            SecondValue = 2,

            [Display(Name = "Third value")]
            ThirdValue = 3
        }
    }
}
