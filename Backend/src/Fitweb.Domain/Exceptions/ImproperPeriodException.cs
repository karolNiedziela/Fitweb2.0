using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exceptions
{
    public class ImproperPeriodException : AppException
    {
        public override string ErrorCode => "improper_period";

        public ImproperPeriodException(string message) : base(message)
        {

        }
    }
}
