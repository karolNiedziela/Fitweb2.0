using Fitweb.Application.Constants;
using Fitweb.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.PipelineBehaviors
{
    public class UserInformationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly HttpContext _httpContext;

        public UserInformationPipelineBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<TResponse> Handle(
            TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {       
            if (request is AuthorizeRequest authorizeRequest)
            {
                var userId = _httpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                var isAdmin = _httpContext?.User?.IsInRole(Roles.Administrator);

                authorizeRequest.UserId = userId;
                authorizeRequest.IsAdmin = isAdmin.Value;
            }

            return await next();
        }
    }
}
