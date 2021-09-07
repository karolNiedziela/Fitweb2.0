using Fitweb.Domain.Common;
using Fitweb.Domain.FoodProducts.Repositories;
using Fitweb.Infrastructure.Persistence.Initializers;
using Fitweb.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence
{
    public static class PersistenceInstaller
    {
        public static IServiceCollection InstallPersistence(this IServiceCollection services)
        {
            IConfiguration configuration;

            using var serviceProvider = services.BuildServiceProvider();
            configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<FitwebDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                options.EnableSensitiveDataLogging(); // TODO: PRODUCTION: TO REMOVE
                options.EnableDetailedErrors(); // TODO: PRODUCTION: TO REMOVE
            });

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IFoodProductRepository, FoodProductRepository>();
            services.AddScoped<IDataInitializer, FoodProductInitializer>();
            services.AddScoped<ISeedData, SeedData>();

            return services;
        }
    }
}
