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
    public class UsersWithRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        private const string adminRoleId = "a792b6cb-8230-4a37-9353-1a05d642ffe2";
        private const string adminUserId = "ff48a62e-0e06-47a2-aacb-c88af07993ed";

        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            var userRole = new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            };

            builder.HasData(userRole);
        }
    }
}
