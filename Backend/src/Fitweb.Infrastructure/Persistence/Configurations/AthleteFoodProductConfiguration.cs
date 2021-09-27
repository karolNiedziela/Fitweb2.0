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
    public class AthleteFoodProductConfiguration : IEntityTypeConfiguration<AthleteFoodProduct>
    {
        public void Configure(EntityTypeBuilder<AthleteFoodProduct> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Athlete)
                   .WithMany(x => x.FoodProducts)
                   .HasForeignKey(x => x.AthleteId);

            builder.HasIndex(x => new { x.AthleteId, x.FoodProductId }).IsUnique(false);
        }
    }
}
