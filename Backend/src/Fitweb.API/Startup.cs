using Fitweb.API.Filters;
using Fitweb.API.Middlewares;
using Fitweb.API.Services;
using Fitweb.Application;
using Fitweb.Application.Interfaces;
using Fitweb.Application.Settings;
using Fitweb.Infrastructure;
using Fitweb.Infrastructure.Persistence;
using Fitweb.Infrastructure.Persistence.Initializers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;

namespace Fitweb.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = true;
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            })
            // To catch FluentValidaiton.ValidationException, it is necessary to set SuppressModelStateInvalidFilter,
            // because default behavior is enabled without setting this flag to true
            // Information: Validation errors automatically trigger an HTTP 400 response.
            // Link: https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-2.1#automatic-http-400-responses
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddFluentValidation(configuration => 
                configuration.RegisterValidatorsFromAssembly(typeof(ApplicationInstaller).Assembly));
            


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fitweb.API", Version = "v1" });
                c.DescribeAllParametersInCamelCase();
 
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization using Bearer.\r\n\r\nEnter Bearer + [space] + token in the text input." +
                    "\r\n\r\nExample: Bearer token"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                c.SchemaFilter<DefaultValueSchemaFilter>();
                c.SchemaFilter<EnumSchemFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();

            services.AddApplication();
            services.AddInfrastructure();

            services.AddCors(options =>
            {
                options.AddPolicy("FitwebOrigin", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddTransient<GlobalErrorHandlerMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<FitwebDbContext>();
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fitweb.API v1");
                    c.DisplayRequestDuration();
                });
            }

            app.UseCors("FitwebOrigin");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<GlobalErrorHandlerMiddleware>();

            var scope = app.ApplicationServices.CreateScope();

            var generalSettings = scope.ServiceProvider.GetRequiredService<GeneralSettings>();
            if (generalSettings.SeedData)
            {
                var seedData = scope.ServiceProvider.GetRequiredService<ISeedData>();
                seedData.SeedAsync();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
