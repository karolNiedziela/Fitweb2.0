using Fitweb.Domain.Athletes;
using Fitweb.Domain.Common;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Exercises;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Trainings
{
    public class Training : Entity
    {
        public int AthleteId { get; private set; }

        public Athlete Athlete { get; private set; }

        public Information Information { get; private set; }

        public DateTime Date { get; private set; }

        public Day Day { get; private set; }

        public List<TrainingExercise> Exercises { get; private set; } = new();

        protected Training()
        {

        }
   
        public Training(Information information, Day day, DateTime? date = null)
        {
            Information = information;
            Day = day;
            Date = date ?? DateTime.UtcNow;
        }

        public void Update(Training training)
        {
            Information = Information.Update(training.Information);
            Day = training.Day;
            Date = training.Date;
        }

        public void AddExercise(Exercise exercise, List<Set> sets)
        {
            var existingExercise = Exercises.SingleOrDefault(x => x.ExerciseId == exercise.Id);
            if (existingExercise is not null)
            {
                throw new AlreadyExistsException(nameof(Exercise), exercise.Information.Name);
            }

            Exercises.Add(new TrainingExercise(exercise, this, sets));
        }

        public TrainingExercise RemoveExercise(int exerciseId)
        {
            var existingExercise = Exercises.SingleOrDefault(x => x.ExerciseId == exerciseId);
            if (existingExercise is null)
            {
                throw new NotFoundException(nameof(Exercise), exerciseId);
            }

            Exercises.Remove(existingExercise);

            return existingExercise;
        }

        public void UpdateExercise(int exerciseId, List<Set> sets)
        {
            var existingExercise = Exercises.SingleOrDefault(x => x.ExerciseId == exerciseId);
            if (existingExercise is null)
            {
                throw new NotFoundException(nameof(Exercise), exerciseId);
            }

            existingExercise.Sets.Clear();
            existingExercise.Sets.AddRange(sets);
        }

        public Set RemoveSet(int exerciseId, int setId)
        {
            var exercise = Exercises.SingleOrDefault(x => x.ExerciseId == exerciseId);
            if (exercise is null)
            {
                throw new NotFoundException(nameof(Exercise), exerciseId);
            }

            var set = exercise.Sets.SingleOrDefault(x => x.Id == setId);
            if (set is null)
            {
                throw new NotFoundException(nameof(Set), setId);
            }

            exercise.Sets.Remove(set);

            return set;
        }
    }
}
