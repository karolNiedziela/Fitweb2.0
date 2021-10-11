using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Exercises;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.UnitTests.Builders;
using Fitweb.Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Domain.UnitTests.Trainings
{
    public class TrainingTests
    {
        [Fact]
        public void CallingTrainingConstructor_ShouldCreateTrainingObject_WhenParametersAreValid()
        {
            var training = new Training(Information.Create("Test", "Test"), Day.Friday);

            training.Information.Name.Should().Be("Test");
            training.Information.Description.Should().Be("Test");
            training.Day.Should().Be(Day.Friday);
            training.Exercises.Count.Should().Be(0);
        }

        [Fact]
        public void Update_ShouldUpdateTraining()
        {
            var training = new Training(Information.Create("test_training"), Day.Wednesday, new DateTime(2019, 8, 20, 10, 10, 10));

            var updatedTraining = new Training(Information.Create("updated_name"), Day.Monday, new DateTime(2019, 2, 20, 10, 10, 10));

            training.Update(updatedTraining);

            training.Should().BeEquivalentTo(updatedTraining);
        }

        [Fact]
        public void AddExercise_ShouldAddExerciseToTraining_WhenExerciseWasNotAdded()
        {
            var exercise = new Exercise(Information.Create("testExercise", null), PartOfBody.Biceps);
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            var sets = new List<Set>() { new Set(40, 5) };
            training.AddExercise(exercise, sets);

            training.Exercises.Count.Should().Be(1);
            training.Exercises.First().Sets.Count.Should().Be(1);
            training.Exercises.FirstOrDefault().Exercise.Should().BeEquivalentTo(exercise);
        }

        [Fact]
        public void AddExercise_ShouldThrowException_WhenExerciseAlreadyExists()
        {
            var exercise = new Exercise(Information.Create("testExercise", null), PartOfBody.Biceps);
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            var sets = new List<Set>() { new Set(40, 5) };
            training.AddExercise(exercise, sets);

            var exception = Record.Exception(() => training.AddExercise(exercise, sets));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<AlreadyExistsException>();
            exception.Message.Should().Be($"Exercise with name: '{exercise.Information.Name}' already exists.");
            training.Exercises.Count.Should().Be(1);
        }

        [Fact]
        public void RemoveExercise_ShouldRemoveExerciseFromList_WhenExerciseExists()
        {
            var exercise = new Exercise(Information.Create("testExercise", null), PartOfBody.Biceps)
            {
                Id = 1
            };
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            var sets = new List<Set>() { new Set(40, 5) };
            training.AddExercise(exercise, sets);

            var countBeforeRemoving = training.Exercises.Count;

            training.RemoveExercise(exercise.Id);

            countBeforeRemoving.Should().Be(1);
            training.Exercises.Count.Should().Be(0);
        }

        [Fact]
        public void RemoveExercise_ShouldThrowException_WhenExerciseDoesNotExist()
        {
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);

            var exception = Record.Exception(() => training.RemoveExercise(5));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be($"Exercise with id: '{5}' was not found.");
        }

        [Fact]
        public void UpdateExercise_ShouldUpdateTrainingExercise_WhenExerciseExists()
        {
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            var exercise = new Exercise(Information.Create("testExercise", null), PartOfBody.Biceps)
            {
                Id = 1
            };
            var sets = new List<Set>() { new Set(40, 5) };
            training.AddExercise(exercise, sets);

            var updatedSets = new List<Set>() { new Set(50, 6) };
            training.UpdateExercise(1, updatedSets);

            training.Exercises.FirstOrDefault().Sets.FirstOrDefault().Weight.Should().Be(50);
            training.Exercises.FirstOrDefault().Sets.FirstOrDefault().NumberOfReps.Should().Be(6);
        }

        [Fact]
        public void UpdateExercise_ShouldThrowException_WhenExerciseDoesNotExist()
        {
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            var sets = new List<Set>() { new Set(40, 5) };
            var exception = Record.Exception(() =>
                training.UpdateExercise(8, sets));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Exercise with id: '8' was not found.");
        }

        [Fact]
        public void RemoveSet_ShouldRemoveSetFromExercise_WhenExerciseExists()
        {
            var training = TrainingBuilder.BuildWithExercisesAndSets();

            var countBeforeRemoving = training.Exercises.FirstOrDefault().Sets.Count;

            training.RemoveSet(1, 1);

            countBeforeRemoving.Should().Be(2);
            training.Exercises.FirstOrDefault().Sets.Count.Should().Be(1);
        }

        [Fact]
        public void RemoveSet_ShouldThrowException_WhenExerciseDoesNotExist()
        {
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);

            var exception = Record.Exception(() => training.RemoveSet(1, 1));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be($"Exercise with id: '{1}' was not found.");
        }

        [Fact]
        public void RemoveSet_ShouldThrowException_WhenSetDoesNotExist()
        {
            var training = new Training(Information.Create("training"), Day.Monday);
            training.AddExercise(new Exercise(Information.Create("exercise"), PartOfBody.Chest)
            {
                Id = 1
            }, 
            new List<Set>
            {
                new Set(25, 5)
            });

            var exception = Record.Exception(() => training.RemoveSet(1, 5));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be($"Set with id: '{5}' was not found.");
        }
    }
}
