using Fitweb.Domain.Trainings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitweb.Infrastructure.Persistence.Configurations
{
    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Exercises)
                   .WithOne(x => x.Training)
                   .HasForeignKey(x => x.TrainingId);

        }
    }
}
