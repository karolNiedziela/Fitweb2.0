using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Common;
using Fitweb.Domain.Exercises.Repositories;
using Fitweb.Domain.FoodProducts.Repositories;
using Fitweb.Domain.Trainings.Repositories;
using Fitweb.Infrastructure.Persistence.Initializers;
using Fitweb.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())); // TODO: PRODUCTION: TO REMOVE
            });

            // Data initializers
            services.AddScoped<IDataInitializer, FoodProductInitializer>();
            services.AddScoped<IDataInitializer, ExerciseInitializer>();
            services.AddScoped<ISeedData, SeedData>();


            // Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IFoodProductRepository, FoodProductRepository>();
            services.AddScoped<IAthleteRepository, AthleteRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();

            return services;
        }
    }
}
