using Fitweb.Application.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            IConfiguration configuration;

            using var serviceProvider = services.BuildServiceProvider();
            configuration = serviceProvider.GetService<IConfiguration>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            var generalSettings = new GeneralSettings();
            configuration.GetSection(GeneralSettings.General).Bind(generalSettings);
            services.AddSingleton(generalSettings);

            return services;
        }
    }
}
