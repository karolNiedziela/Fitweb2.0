using Fitweb.Domain.Athletes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.UnitTests.Builders
{
    public class AthleteBuilder
    {
        public const string DefaultUserId = "user_id";


        public static List<Athlete> Athletes { get; set; } = new();

        public static Athlete Build(string userId = DefaultUserId)
        {
            return new Athlete(userId);
        }

        public static Athlete BuildWithNullableParameters(string userId = DefaultUserId, int height = 180, 
            int weight = 80, int numberOfTrainings = 2)
        {
            return new Athlete(userId, height, weight, numberOfTrainings);
        }

        public static Athlete BuildWithTrainings()
        {
            var athlete = Build();
            var training = TrainingBuilder.Build();
            athlete.AddTraining(training);

            return athlete;
        }

        public static Athlete BuildWithFoodProduct(double weight = 100)
        {
            var athlete = Build();
            var foodProduct = FoodProductBuilder.Build();
            athlete.AddFoodProduct(foodProduct, weight);
            athlete.FoodProducts[0].Id = 1;

            return athlete;
        }
    }
}
