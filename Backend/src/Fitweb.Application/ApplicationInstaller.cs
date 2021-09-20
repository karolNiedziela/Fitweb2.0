using AutoMapper;
using Fitweb.Application.Interfaces;
using Fitweb.Application.Mapping;
using Fitweb.Application.PipelineBehaviors;
using Fitweb.Application.PipelineBehaviours;
using Fitweb.Application.Settings;
using FluentValidation.AspNetCore;
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

            var generalSettings = new GeneralSettings();
            configuration.GetSection(GeneralSettings.General).Bind(generalSettings);
            services.AddSingleton(generalSettings);

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserIdPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            return services;
        }
    }
}
