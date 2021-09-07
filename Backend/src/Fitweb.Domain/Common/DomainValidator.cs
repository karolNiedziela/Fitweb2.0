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
        public static void AgainstEmptyString(string value, string name = "Value")
        {
            if (!string.IsNullOrEmpty(value))
            {
                return;
            }

            throw new EmptyStringException($"{name} cannnot be null or empty.");
        }

        public static void AgainstNegativeNumber(double value, string name = "Value")
        {
            if (value > 0)
            {
                return;
            }

            throw new NegativeNumberException($"{name} cannot be negative.");
        }


    }
}
