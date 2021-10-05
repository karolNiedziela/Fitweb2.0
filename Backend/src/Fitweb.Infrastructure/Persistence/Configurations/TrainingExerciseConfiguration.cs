using Fitweb.Domain.Trainings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitweb.Infrastructure.Persistence.Configurations
{
    public class TrainingExerciseConfiguration : IEntityTypeConfiguration<TrainingExercise>
    {
        public void Configure(EntityTypeBuilder<TrainingExercise> builder)
        {
            builder.HasKey(x => new { x.ExerciseId, x.TrainingId });

            builder.HasMany(x => x.Sets)
                   .WithOne(x => x.TrainingExercise)
                   .HasForeignKey(x => new { x.ExerciseId, x.TrainingId });
        }
    }
}
