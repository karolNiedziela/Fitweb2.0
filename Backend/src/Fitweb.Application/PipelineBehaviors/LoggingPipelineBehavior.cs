using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.PipelineBehaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            var type = request.GetType();
            var properties = new List<PropertyInfo>(type.GetProperties());     
            var stringBuilder = new StringBuilder();
            foreach (var property in properties)
            {
                //TODO: probably should be removed, keep for information purpose
                if (property.Name != "Password")
                {
                    var propertyValue = property.GetValue(request, null);
                    stringBuilder.Append($"{property.Name} : {propertyValue} ");
                }
            }

            _logger.LogInformation(stringBuilder.ToString());

            var response = await next();
            var responseType = typeof(TResponse);
            //TODO: rethink it
            if (responseType.GenericTypeArguments.Any()) 
            {
                var resultTypeof = responseType.Name.Trim(new char[] { '`', '1' }) + $"<{responseType.GenericTypeArguments[0].Name}>";
                _logger.LogInformation($"Handled {resultTypeof}");
            }
            else
            {
               _logger.LogInformation($"Handled {typeof(TResponse).Name.Trim(new char[] { '`', '1' })}");
            }

            return response;
        }
    }
}
