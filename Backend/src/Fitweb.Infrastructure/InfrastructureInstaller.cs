using Fitweb.Application.Interfaces;
using Fitweb.Infrastructure.Email;
using Fitweb.Infrastructure.Exceptions;
using Fitweb.Infrastructure.Identity;
using Fitweb.Infrastructure.Persistence;
using Fitweb.Infrastructure.Shared;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }
    }
}
