using Fitweb.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Configurations
{
    public class AdministratorConfiguration : IEntityTypeConfiguration<User>
    {
        private const string adminId = "ff48a62e-0e06-47a2-aacb-c88af07993ed";

        public void Configure(EntityTypeBuilder<User> builder)
        {
            var admin = new User
            {
                Id = adminId,
                UserName = "administrator",
                NormalizedUserName = "ADMINISTRATOR",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
            };

            admin.PasswordHash = GeneratePassword(admin);

            builder.HasData(admin);
        }

        private string GeneratePassword(User user)
        {
            var passwordHasher = new PasswordHasher<User>();

            return passwordHasher.HashPassword(user, "adminPassword");
        }
    }
}
