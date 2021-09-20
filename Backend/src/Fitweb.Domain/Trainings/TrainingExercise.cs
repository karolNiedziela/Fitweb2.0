using Fitweb.Domain.Common;
using Fitweb.Domain.Exercises;
using System.Collections.Generic;

namespace Fitweb.Domain.Trainings
{
    public class TrainingExercise
    {
        public int TrainingId { get; private set; }

        public Training Training { get; private set; }

        public int ExerciseId { get; private set; }

        public Exercise Exercise { get; private set; }

        public List<Set> Sets { get; private set; } = new();

        protected TrainingExercise()
        {

        }

        public TrainingExercise(Exercise exercise, Training training)
        {
            Exercise = exercise;
            Training = training;
        }
    }
}