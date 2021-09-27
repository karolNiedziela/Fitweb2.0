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
    public class AthleteConfiguration : IEntityTypeConfiguration<Athlete>
    {
        public void Configure(EntityTypeBuilder<Athlete> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Trainings)
                   .WithOne(x => x.Athlete)
                   .HasForeignKey(x => x.AthleteId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.DietInformations)
                   .WithOne(x => x.Athlete)
                   .HasForeignKey(x => x.AthleteId);
        }
    }
}
