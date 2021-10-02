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
            Value = DomainValidator.AgainstNegativeNumber(value, "Calories");
        }

        public static Calories Update(Calories calories)
        {
            return calories with { Value = calories.Value };
        }

        public static Calories Create(double value)
            => new(value);
    }
}
