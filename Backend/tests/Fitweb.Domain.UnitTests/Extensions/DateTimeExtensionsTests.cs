using Fitweb.Domain.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Domain.UnitTests.Extensions
{
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData("2021-10-10", "2021-10-10", "2021-11-06")]
        [InlineData("2021-10-10", "2021-05-04", "2021-11-21")]
        public void IsIsRange_ShouldReturnTrue_WhenPeriodIsInRange(string toCheck, string start, string end)
        {
            var dateToCheck = DateTime.Parse(toCheck);

            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);

            var result = dateToCheck.IsInRange(startDate, endDate);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("2021-10-10", "2021-10-10", null)]
        [InlineData("2021-10-10", "2021-10-10", "2021-12-12")]
        public void IsInRangeWithNullables_ShouldReturnTrue_WhenPeriodIsInRange(string toCheck, string start, string end)
        {
            var dateToCheck = DateTime.Parse(toCheck);
            DateTime? startDate = string.IsNullOrEmpty(start) ? null : DateTime.Parse(start);
            DateTime? endDate = string.IsNullOrEmpty(end) ? null : DateTime.Parse(end);

            var result = dateToCheck.IsInRange(startDate, endDate);

            result.Should().BeTrue();
        }
    }
}
