using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        public abstract string ErrorCode { get; }

        protected DomainException(string message) : base(message) { }
    }
}
