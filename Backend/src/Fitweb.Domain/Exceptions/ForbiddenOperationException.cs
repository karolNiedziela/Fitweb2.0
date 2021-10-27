using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exceptions
{
    public class ForbiddenOperationException : AppException
    {
        public override string ErrorCode => "prohibited_operation";

        public ForbiddenOperationException() : base("Prohibited operation")
        {
        }
    }
}
