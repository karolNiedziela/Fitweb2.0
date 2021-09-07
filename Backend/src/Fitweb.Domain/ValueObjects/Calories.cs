using Fitweb.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.ValueObjects
{
    public record Calories
    {
        public double Value { get; init; }

        protected Calories()
        {

        }

        private Calories(double value)
        {
            DomainValidator.AgainstNegativeNumber(value, "Calories");

            Value = value;
        }

        public static Calories Create(double value)
            => new(value);
    }
}
