using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exceptions
{
    public class NegativeOrZeroNumberException : AppException
    {
        public override string ErrorCode => "negative_or_zero_number";

        public NegativeOrZeroNumberException(string message) : base(message)
        {

        }
    }
}
