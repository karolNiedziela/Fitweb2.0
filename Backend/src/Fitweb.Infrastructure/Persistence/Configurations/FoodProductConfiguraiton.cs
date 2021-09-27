using Fitweb.Domain.FoodProducts;
using Fitweb.Infrastructure.Persistence.Initializers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Configurations
{
    public class FoodProductConfiguraiton : IEntityTypeConfiguration<FoodProduct>
    {

        public void Configure(EntityTypeBuilder<FoodProduct> builder)
        {
            builder.ToTable("FoodProducts");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Information, navigation =>
            {
                navigation.Property(x => x.Name)
                    .HasColumnName("Name");

                navigation.Property(x => x.Description)
                    .HasColumnName("Description");
            });
                
            builder.OwnsOne(x => x.Calories, navigation =>
            {
                navigation.Property(x => x.Value)
                    .HasColumnName("Calories");
            });

            builder.OwnsOne(x => x.Nutrient, navigation =>
            {
                navigation.Property(x => x.Protein)
                    .HasColumnName("Protein");

                navigation.Property(x => x.Carbohydrate)
                    .HasColumnName("Carbohydrate");

                navigation.Property(x => x.Fat)
                    .HasColumnName("Fat");

                navigation.Property(x => x.SaturatedFat)
                    .HasColumnName("SaturatedFat");

                navigation.Property(x => x.Sugar)
                    .HasColumnName("Sugar");

                navigation.Property(x => x.Fiber)
                    .HasColumnName("Fiber");

                navigation.Property(x => x.Salt)
                    .HasColumnName("Salt");
            });

            builder.Property(x => x.UserId).IsRequired(false);
        }
    }
}
