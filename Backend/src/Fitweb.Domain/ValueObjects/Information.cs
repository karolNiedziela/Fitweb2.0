using Fitweb.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.ValueObjects
{
    public record Information
    {
        public string Name { get; init; }

        public string Description { get; init; }

        protected Information()
        {

        }

        private Information(string name, string description)
        {
            DomainValidator.AgainstEmptyString(name, "Name");

            Name = name;
            Description = description;
        }

        public static Information Create(string name, string description)
            => new(name, description);
    }
}
