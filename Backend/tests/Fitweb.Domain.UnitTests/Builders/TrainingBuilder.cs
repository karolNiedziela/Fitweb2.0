using Fitweb.Domain.Trainings;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.UnitTests.Builders
{
    public class TrainingBuilder
    {
        public static Training Build(string name = "test_training", string description = "test_description",
            Day day = Day.Monday)
        {
            return new Training(Information.Create(name, description), day)
            {
                Id = 1
            };
        }

        public static Training BuildWithExercisesAndSets()
        {
            var training = Build();
            var exercise = ExerciseBuilder.Build();
            var sets = new List<Set>
            {
                new Set(50, 10) 
                {
                    Id = 1
                },
                new Set(60, 8)
                {
                    Id = 2
                }
            };
            training.AddExercise(exercise, sets);

            return training;
        }
    }
}
