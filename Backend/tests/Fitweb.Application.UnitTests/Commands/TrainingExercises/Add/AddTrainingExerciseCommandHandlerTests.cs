using Fitweb.Application.Commands.TrainingExercises.Add;
using Fitweb.Application.Responses;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Exercises;
using Fitweb.Domain.Exercises.Repositories;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using Fitweb.Domain.ValueObjects;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.TrainingExercises.Add
{
    public class AddTrainingExerciseCommandHandlerTests
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly AddTrainingExerciseCommandHandler _sut;

        public AddTrainingExerciseCommandHandlerTests()
        {
            _trainingRepository = Substitute.For<ITrainingRepository>();
            _exerciseRepository = Substitute.For<IExerciseRepository>();
            _sut = new AddTrainingExerciseCommandHandler(_trainingRepository, _exerciseRepository);
        }

        [Fact]
        public async Task Handle_ShouldAddTrainingExerciseToTraining_IfTrainingAndExerciseExists()
        {
            var training = new Training(Information.Create("Test training", null), Day.Saturday)
            {
                Id = 1
            };

            _trainingRepository.GetExercisesWithSets(Arg.Any<string>(), Arg.Any<int>()).Returns(training);

            var exercise = new Exercise(Information.Create("Test exercise", null), null)
            {
                Id = 1
            };

            _exerciseRepository.GetByIdAsync(Arg.Any<int>()).Returns(exercise);

            var result = await _sut.Handle(new AddTrainingExerciseCommand
            {
                UserId = "testUserId",
                TrainingId = 1,
                ExerciseId = 1,
            });

            result.Should().BeOfType<Response<string>>();
            result.Message.Should().Be("Training exercise added successfully.");
            training.Exercises.Count.Should().Be(1);
            await _trainingRepository.Received(1).UpdateAsync(Arg.Is<Training>(x =>
                x.Id == 1 &&
                x.Information.Name == "Test training" &&
                x.Exercises.FirstOrDefault().ExerciseId == 1));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenTrainingDoesNotExist()
        {
            _trainingRepository.GetExercisesWithSets(Arg.Any<string>(), Arg.Any<int>()).ReturnsNull();

            var exception = await Record.ExceptionAsync(() => _sut.Handle(new AddTrainingExerciseCommand
            {
                UserId = "testUserId",
                TrainingId = 1,
                ExerciseId = 1
            }));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Training with id: '1' was not found.");
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenExerciseDoesNotExist()
        {
            var training = new Training(Information.Create("Test training", null), Day.Sunday);

            _trainingRepository.GetExercisesWithSets(Arg.Any<string>(), Arg.Any<int>()).Returns(training);

            _exerciseRepository.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

            var exception = await Record.ExceptionAsync(() => _sut.Handle(new AddTrainingExerciseCommand
            {
                UserId = "testUserId",
                TrainingId = 1,
                ExerciseId = 1
            }));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Exercise with id: '1' was not found.");
        }
    }
}
