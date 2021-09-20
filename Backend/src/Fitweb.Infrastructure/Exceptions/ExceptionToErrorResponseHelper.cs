using Fitweb.Application.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Exceptions
{
    public static class ExceptionToErrorResponseHelper
    {
        public static ErrorResponse MapIdentityError(IdentityResult result)
        {
            var errorResponse = CreateBadRequestResponse("identity_error");
            foreach (var error in result.Errors)
            {
                errorResponse.Errors.Add(error.Description);
            }

            return errorResponse;
        }

        public static ErrorResponse MapFluentValidationException(ValidationException validationException)
        {
            var errorResponse = CreateBadRequestResponse("validation_error");
            foreach (var error in validationException.Errors)
            {
                errorResponse.Errors.Add(error.ErrorMessage);
            }

            return errorResponse;
        }

        private static ErrorResponse CreateBadRequestResponse(string code)
        {
            return new ErrorResponse
            {
                Code = code,
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}
