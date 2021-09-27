using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Exercises;
using Fitweb.Domain.Trainings;
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
        public void AddExercise_ShouldAddExerciseToTraining_WhenExerciseWasNotAdded()
        {
            var exercise = new Exercise(Information.Create("testExercise", null), PartOfBody.Biceps);
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);

            training.AddExercise(exercise);

            training.Exercises.Count.Should().Be(1);
            training.Exercises.FirstOrDefault().Exercise.Should().BeEquivalentTo(exercise);
        }

        [Fact]
        public void AddExercise_ShouldThrowException_WhenExerciseAlreadyExists()
        {
            var exercise = new Exercise(Information.Create("testExercise", null), PartOfBody.Biceps);
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            training.AddExercise(exercise);

            var exception = Record.Exception(() => training.AddExercise(exercise));

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
            training.AddExercise(exercise);

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
        public void AddSet_ShouldAddSetToExercise_WhenExerciseExists()
        {
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            var exercise = new Exercise(Information.Create("testExercise", null), PartOfBody.Biceps)
            {
                Id = 1
            };

            training.AddExercise(exercise);
            var set = new Set(20, 3);

            training.AddSet(exercise.Id, set);

            training.Exercises.FirstOrDefault().Sets.Count.Should().Be(1);
        }

        [Fact]
        public void AddSet_ShouldThrowException_WhenExerciseDoesNotExist()
        {
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            var set = new Set(20, 3);

            var exception = Record.Exception(() => training.AddSet(5, set));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be($"Exercise with id: '{5}' was not found.");
        }

        [Fact]
        public void RemoveSet_ShouldRemoveSetFromExercise_WhenExerciseExists()
        {
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            var exercise = new Exercise(Information.Create("testExercise", null), PartOfBody.Biceps)
            {
                Id = 1
            };
            training.AddExercise(exercise);
            var set = new Set(20, 3)
            {
                Id = 1
            };
            training.AddSet(exercise.Id, set);

            var countBeforeRemoving = training.Exercises.FirstOrDefault().Sets.Count;

            training.RemoveSet(exercise.Id, set.Id);

            countBeforeRemoving.Should().Be(1);
            training.Exercises.FirstOrDefault().Sets.Count.Should().Be(0);
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
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            var exercise = new Exercise(Information.Create("testExercise", null), PartOfBody.Biceps)
            {
                Id = 1
            };
            training.AddExercise(exercise);

            var exception = Record.Exception(() => training.RemoveSet(exercise.Id, 1));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be($"Set with id: '{1}' was not found.");
        }
    }
}
