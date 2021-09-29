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
        public void UpdateExercise_ShouldUpdateTrainingExercise_WhenExerciseExists()
        {
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);
            var exercise = new Exercise(Information.Create("testExercise", null), PartOfBody.Biceps)
            {
                Id = 1
            };
            training.AddExercise(exercise);

            training.UpdateExercise(1, new Exercise(Information.Create("new name", null), PartOfBody.Legs)
            {
                Id = 1
            });

            training.Exercises[0].Exercise.Information.Name.Should().Be("new name");
            training.Exercises[0].Exercise.PartOfBody.Should().Be(PartOfBody.Legs);
        }

        [Fact]
        public void UpdateExercise_ShouldThrowException_WhenExerciseDoesNotExist()
        {
            var training = new Training(Information.Create("Test", "Test"), Day.Tuesday);

            var exception = Record.Exception(() => 
                training.UpdateExercise(8, new Exercise(Information.Create("test"), null)));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Exercise with id: '8' was not found.");
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

        [Fact]
        public void UpdateSet_ShouldUpdateSet_WhenExerciseAndSetExists()
        {
            var training = new Training(Information.Create("test training"), Day.Thursday);
            training.AddExercise(new Exercise(Information.Create("test exercise"), PartOfBody.Chest)
            {
                Id = 1
            });
            training.AddSet(1, new Set(25, 4)
            {
                Id = 1
            });

            training.UpdateSet(1, 1, 50, 3 , 1);

            training.Exercises[0].Sets[0].Weight.Should().Be(50);
            training.Exercises[0].Sets[0].NumberOfReps.Should().Be(3);
            training.Exercises[0].Sets[0].NumberOfSets.Should().Be(1);
        }

        [Fact]
        public void UpdateSet_ShouldThrowException_WhenExerciseDoesNotExist()
        {
            var training = new Training(Information.Create("test training"), Day.Thursday);

            var exception = Record.Exception(() => training.UpdateSet(5, 10, 150, 2, 1));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be($"Exercise with id: '{5}' was not found.");
        }

        [Fact]
        public void UpdateSet_ShouldThrowException_WhenExerciseExistsButSetDoesNotExist()
        {
            var training = new Training(Information.Create("test training"), Day.Thursday);
            training.AddExercise(new Exercise(Information.Create("test exercise"), PartOfBody.Chest)
            {
                Id = 1
            });

            var exception = Record.Exception(() => training.UpdateSet(1, 7, 150, 2, 1));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be($"Set with id: '{7}' was not found.");
        }
    }
}
