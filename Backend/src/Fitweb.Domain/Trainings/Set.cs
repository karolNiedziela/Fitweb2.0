using Fitweb.Domain.Common;
using System.Collections.Generic;

namespace Fitweb.Domain.Trainings
{
    public class Set : Entity
    {

        public double Weight { get; private set; }

        public int NumberOfReps { get; private set; }

        // Sometimes someone performs a series with the same weight
        public int NumberOfSets { get; private set; } = 1;

        public TrainingExercise TrainingExercise { get; private set; }

        public int ExerciseId { get; private set; }

        public int TrainingId { get; private set; }

        protected Set()
        {

        }

        public Set(double weight, int numberOfReps, int numberOfSets = 1)
        {
            Weight = DomainValidator.AgainstNegativeAndZeroNumber(weight, nameof(Weight));
            NumberOfReps = DomainValidator.AgainstNegativeAndZeroNumber(numberOfReps, "Number of reps");
            NumberOfSets = DomainValidator.AgainstNegativeAndZeroNumber(numberOfSets, "Number of sets");
        }

        public void Update(double weight, int numberOfReps, int numberOfSets)
        {
            Weight = DomainValidator.AgainstNegativeAndZeroNumber(weight, nameof(Weight));
            NumberOfReps = DomainValidator.AgainstNegativeAndZeroNumber(numberOfReps, "Number of reps");
            NumberOfSets = DomainValidator.AgainstNegativeAndZeroNumber(numberOfSets, "Number of sets");
        }
    }
}