using Fitweb.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Fitweb.API.Middlewares
{
    public class GlobalErrorHandlerMiddleware : IMiddleware
    {
        private readonly IExceptionToErrorResponseMapper _exceptionMapper;

        public GlobalErrorHandlerMiddleware(IExceptionToErrorResponseMapper exceptionMapper)
        {
            _exceptionMapper = exceptionMapper;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception  exception)
        {
            var errorResponse = _exceptionMapper.Map(exception);
            context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);

            var isResponseEmpty = errorResponse.Errors.Count == 0;

            if (isResponseEmpty)
            {
                await context.Response.WriteAsync(string.Empty);
            }

            context.Response.ContentType = "application/json";

            //TODO: Probably status code is to remove
            var payload = JsonConvert.SerializeObject(new { code = errorResponse.Code, statusCode = context.Response.StatusCode,
                errors = new List<string>(errorResponse.Errors) });

            await context.Response.WriteAsync(payload);
        }
    }
}
