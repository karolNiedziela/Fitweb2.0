using Fitweb.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Common
{
    public static class DomainValidator
    {
        public static string AgainstEmptyString(string value, string name = "Value")
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }

            throw new EmptyStringException($"{name} cannot be null or empty.");
        }

        public static double AgainstNegativeNumber(double value, string name = "Value")
        {
            if (value >= 0)
            {
                return value;
            }

            throw new NegativeNumberException($"{name} cannot be negative.");
        }


        public static double? AgainstNegativeNumber(double? value, string name = "Value")
        {
            if (!value.HasValue)
            {
                return null;
            }

            if (value >= 0)
            {
                return value.Value;
            }

            throw new NegativeNumberException($"{name} cannot be negative.");
        }

        public static int? AgainstNegativeNumber(int? value, string name = "Value")
        {
            if (!value.HasValue)
            {
                return null;
            }

            if (value >= 0)
            {
                return value.Value;
            }

            throw new NegativeNumberException($"{name} cannot be negative.");
        }

        public static int AgainstNegativeAndZeroNumber(int value, string name = "Value")
        {
            if (value > 0)
            {
                return value;
            }

            throw new NegativeOrZeroNumberException($"{name} cannot be negative or zero.");
        }

        public static double AgainstNegativeAndZeroNumber(double value, string name = "Value")
        {
            if (value > 0)
            {
                return value;
            }

            throw new NegativeOrZeroNumberException($"{name} cannot be negative or zero.");
        }

        public static double AgainstNegativeAndZeroNumber(double? value, string name = "Value")
        {
            if (value > 0)
            {
                return value.Value;
            }

            throw new NegativeOrZeroNumberException($"{name} cannot be negative or zero.");
        }      

        public static void AgainstImproperPeriod(DateTime? startDate, DateTime? endDate)
        {
            // Both do not have value
            if (!startDate.HasValue && !endDate.HasValue)
            {
                return;
            }

            // Start date is defined and end date not
            if (startDate.HasValue && !endDate.HasValue)
            {
                return;
            }

            // Start date and end date are defined and end date is later than start date
            if (startDate.HasValue && endDate.HasValue && startDate < endDate)
            {
                return;
            }

            throw new ImproperPeriodException("Incorrect time interval.");
        }
    }
}
