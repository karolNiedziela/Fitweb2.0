using Fitweb.Domain.Exceptions;
using Fitweb.Application.Responses;
using Fitweb.Infrastructure.Identity.Exceptions;
using Fitweb.Infrastructure.Exceptions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Exceptions
{
    public class ExceptionToErrorResponseMapper : IExceptionToErrorResponseMapper
    {
        public ErrorResponse Map(Exception exception)
            => exception switch
            {
                AppException ex => new ErrorResponse(ex.ErrorCode, HttpStatusCode.BadRequest, ex.Message),

                NotFoundException ex => new ErrorResponse(ex.ErrorCode, HttpStatusCode.NotFound, ex.Message),

                IdentityException ex => ExceptionToErrorResponseHelper.MapIdentityError(ex.Error),

                ValidationException ex => ExceptionToErrorResponseHelper.MapFluentValidationException(ex),

                //TODO: add discard
            };
    }
}
