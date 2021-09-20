using Fitweb.Domain.Exercises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Configurations
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Information, navigation =>
            {
                navigation.Property(x => x.Name).HasColumnName("Name");

                navigation.Property(x => x.Description).HasColumnName("Description");
            });
        }
    }
}
