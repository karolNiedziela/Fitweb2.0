using Fitweb.Application.Interfaces;
using Fitweb.Infrastructure.Utilities.Email;
using Fitweb.Infrastructure.Exceptions;
using Fitweb.Infrastructure.Identity;
using Fitweb.Infrastructure.Persistence;
using Fitweb.Infrastructure.Shared;
using Microsoft.Extensions.DependencyInjection;
using Fitweb.Application.Interfaces.Utilities.Csv;
using Fitweb.Infrastructure.Utilities.Csv;
using Fitweb.Infrastructure.Persistence.Initializers;

namespace Fitweb.Infrastructure
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddEmailService();
            services.InstallIdentity();
            services.InstallPersistence();


            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IExceptionToErrorResponseMapper, ExceptionToErrorResponseMapper>();

            services.AddScoped(typeof(ICsvService<,>), typeof(CsvService<,>));

            return services;
        }
    }
}
