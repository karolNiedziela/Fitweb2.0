using Fitweb.Domain.Athletes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Configurations
{
    public class DietInformationConfiguration : IEntityTypeConfiguration<DietInformation>
    {
        public void Configure(EntityTypeBuilder<DietInformation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.IsCurrent);
        }
    }
}
