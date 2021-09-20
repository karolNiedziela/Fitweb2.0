using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exceptions
{
    public abstract class AppException : Exception
    {
        public abstract string ErrorCode { get; }

        protected AppException(string message) : base(message) { }
    }
}
