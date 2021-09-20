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
    public class UserIdPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly HttpContext _httpContext;

        public UserIdPipelineBehavior(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<TResponse> Handle(
            TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var userId = _httpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (request is AuthorizeRequest authorizeRequest)
            {
                authorizeRequest.UserId = userId;
            }

            return await next();
        }
    }
}
