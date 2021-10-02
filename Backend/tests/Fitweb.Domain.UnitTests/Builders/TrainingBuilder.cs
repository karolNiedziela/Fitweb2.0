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
            return new Training(Information.Create(name, description), day);
        }

        public static Training BuildWithExercises()
        {
            var training = Build();
            var exercise = ExerciseBuilder.Build();
            training.AddExercise(exercise);

            return training;
        }
    }
}
