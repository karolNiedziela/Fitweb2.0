using Fitweb.Application.Commands.TrainingExercises.Update;
using Fitweb.Application.DTO;
using Fitweb.Application.Responses;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Trainings.Repositories;
using Fitweb.Domain.UnitTests.Builders;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.TrainingExercises.Update
{
    public class UpdateTrainingExerciseCommandHandlerTests 
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly UpdateTrainingExerciseCommandHandler _sut;

        public UpdateTrainingExerciseCommandHandlerTests()
        {
            _trainingRepository = Substitute.For<ITrainingRepository>();
            _sut = new UpdateTrainingExerciseCommandHandler(_trainingRepository);
        }

        [Fact]
        public async Task Handle_ShouldUpdateTrainingExercise_WhenTrainingExists()
        {
            var training = TrainingBuilder.BuildWithExercisesAndSets();
            _trainingRepository.GetExercisesWithSets(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(training);

            var response = await _sut.Handle(new UpdateTrainingExerciseCommand
            {
                TrainingId = 1,
                ExerciseId = 1,
                Sets = new List<SetDto>
                {
                    new SetDto
                    {
                        Id = 1,
                        Weight = 100,
                        NumberOfReps = 2,
                        NumberOfSets = 2
                    },
                    new SetDto
                    {
                        Id = 2,
                        Weight = 150,
                        NumberOfReps = 3,
                        NumberOfSets = 1
                    }
                }
            });

            response.Should().BeOfType<Response<string>>();
            response.Message.Should().Be("Training exercise updated successfully.");
            await _trainingRepository.Received(1).UpdateAsync(Arg.Is(training));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenTrainingDoesNotExist()
        {
            var exception = await Record.ExceptionAsync(() => _sut.Handle(new UpdateTrainingExerciseCommand
            {
                TrainingId = 1,
                ExerciseId = 1
            }));

            exception.Should().BeOfType(typeof(NotFoundException));
            exception.Message.Should().Be("Training exercise with id: '1' was not found.");
        }
    }
}
