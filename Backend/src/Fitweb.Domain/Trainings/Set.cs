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

        public int TrainingId { get; set; }

        protected Set()
        {

        }

        public Set(double weight, int numberOfReps, int numberOfSets = 1)
        {
            DomainValidator.AgainstNegativeAndZeroNumber(weight, nameof(Weight));
            DomainValidator.AgainstNegativeAndZeroNumber(numberOfSets, nameof(NumberOfSets));
            DomainValidator.AgainstNegativeAndZeroNumber(numberOfReps, nameof(NumberOfReps));
            Weight = weight;
            NumberOfReps = numberOfReps;
            NumberOfSets = numberOfSets;
        }

        public void SetWeight(double weight)
        {
            DomainValidator.AgainstNegativeAndZeroNumber(weight, nameof(Weight));

            Weight = weight;
        }

        public void SetNumberOfSets(int numberOfSets)
        {
            DomainValidator.AgainstNegativeAndZeroNumber(numberOfSets, nameof(NumberOfSets));

            NumberOfSets = numberOfSets;
        }

        public void SetNumberOfReps(int numberOfReps)
        {
            DomainValidator.AgainstNegativeAndZeroNumber(numberOfReps, nameof(NumberOfReps));

            NumberOfReps = numberOfReps;
        }
    }
}