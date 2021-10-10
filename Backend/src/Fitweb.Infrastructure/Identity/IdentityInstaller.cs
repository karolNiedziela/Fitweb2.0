using Fitweb.Application.Interfaces.Identity;
using Fitweb.Infrastructure.Identity.Entities;
using Fitweb.Infrastructure.Identity.External.Facebook.Services;
using Fitweb.Infrastructure.Identity.External.Facebook.Settings;
using Fitweb.Infrastructure.Identity.Factories;
using Fitweb.Infrastructure.Identity.Services;
using Fitweb.Infrastructure.Identity.Settings;
using Fitweb.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity
{
    public static class IdentityInstaller
    {
        public static IServiceCollection InstallIdentity(this IServiceCollection services)
        {
            IConfiguration configuration;

            using var serviceProvider = services.BuildServiceProvider();
            configuration = serviceProvider.GetService<IConfiguration>();

            // Important to put AddIdentity before AddAuthentication because Identity calls AddAuthentication which cause overidding

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<FitwebDbContext>()
            .AddDefaultTokenProviders();

            var jwtSettings = new JwtSettings();
            configuration.GetSection(JwtSettings.Jwt).Bind(jwtSettings);
            services.AddSingleton(jwtSettings);


            var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = jwtSettings.ValidateAudience,
                //ValidAudience = jwtSettings.Audience, TODO: Set audience
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddSingleton(tokenValidationParameters);

            var facebookSettings = new FacebookSettings();
            configuration.GetSection(FacebookSettings.Facebook).Bind(facebookSettings);
            services.AddSingleton(facebookSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;

                options.TokenValidationParameters = tokenValidationParameters;

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            })
            .AddFacebook(options =>
            {
                options.AppId = facebookSettings.AppId;
                options.AppSecret = facebookSettings.AppSecret;

                options.SignInScheme = IdentityConstants.ExternalScheme;
            });


            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddSingleton<IRefreshTokenFactory, RefreshTokenFactory>();
            services.AddSingleton<IRng, Rng>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IFacebookAuthService, FacebookAuthService>();

            return services;
        } 
    }
}
