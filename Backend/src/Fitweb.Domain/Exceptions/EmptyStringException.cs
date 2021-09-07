using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exceptions
{
    public class EmptyStringException : DomainException
    {
        public override string ErrorCode => "empty_string";

        public EmptyStringException(string message) : base(message)
        {

        }
    }
}
