using Fitweb.Application.Constants;
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
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        private const string adminId = "a792b6cb-8230-4a37-9353-1a05d642ffe2";
        private const string athleteId = "9dd36f65-1fc5-4383-b7af-626d5bd60728";

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = adminId,
                    Name = Roles.Administrator,
                    NormalizedName = Roles.Administrator.ToUpper()
                },
                new IdentityRole
                {
                    Id = athleteId,
                    Name = Roles.Athlete,
                    NormalizedName = Roles.Athlete.ToUpper()
                });
        }
    }
}
