using Fitweb.Application.Interfaces.Utilities.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Utilities.Email
{
    public static class EmailInstaller
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            IConfiguration configuration;

            using var serviceProvider = services.BuildServiceProvider();
            configuration = serviceProvider.GetService<IConfiguration>();

            var emailSmtpSettings = new EmailSettings();
            configuration.GetSection(EmailSettings.Mail).Bind(emailSmtpSettings);

            services.AddSingleton(emailSmtpSettings);

            services.AddTransient<IEmailSender, EmailSender>();
            return services;
        }
    }
}
