using Fitweb.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Exceptions
{
    public interface IExceptionToErrorResponseMapper
    {
        ErrorResponse Map(Exception exception);
    }
}
