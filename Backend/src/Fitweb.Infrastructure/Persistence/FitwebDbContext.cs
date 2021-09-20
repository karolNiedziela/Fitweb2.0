using Fitweb.Application.Interfaces;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Common;
using Fitweb.Domain.Exercises;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.Trainings;
using Fitweb.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence
{
    public class FitwebDbContext : IdentityDbContext<User>
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<FoodProduct> FoodProducts { get; set; }

        public DbSet<Athlete> Athletes { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<TrainingExercise> TrainingExercises { get; set; }

        public DbSet<Set> Sets { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public FitwebDbContext(DbContextOptions<FitwebDbContext> options, IDateTimeProvider dateTimeProvider) 
            : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<Entity> entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTimeProvider.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = _dateTimeProvider.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");            
        }
    }
}
