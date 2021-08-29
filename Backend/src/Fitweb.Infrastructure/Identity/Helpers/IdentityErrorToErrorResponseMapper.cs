using Fitweb.Application.Responses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Helpers
{
    public static class IdentityErrorToErrorResponseMapper
    {
        public static ErrorResponse Map(IdentityResult result)
        {
            var errorResponse = new ErrorResponse();
            foreach (var error in result.Errors)
            {
                errorResponse.Code = "identity_error";
                errorResponse.Errors.Add(error.Description);
            }

            return errorResponse;
        }
    }
}
