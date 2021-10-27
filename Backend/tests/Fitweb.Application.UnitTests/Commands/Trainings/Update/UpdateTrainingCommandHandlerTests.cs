using Fitweb.Application.Commands.Trainings.Update;
using Fitweb.Application.Responses;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Trainings;
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

namespace Fitweb.Application.UnitTests.Commands.Trainings.Update
{
    public class UpdateTrainingCommandHandlerTests
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly UpdateTrainingCommandHandler _sut;

        public UpdateTrainingCommandHandlerTests()
        {
            _trainingRepository = Substitute.For<ITrainingRepository>();
            _sut = new UpdateTrainingCommandHandler(_trainingRepository);
        }

        [Fact]
        public async Task Handle_ShouldUpdateTrainingWithGivenId()
        {
            var training = TrainingBuilder.Build();
            _trainingRepository.GetByIdAsync(Arg.Any<int>()).Returns(training);

            var response = await _sut.Handle(new UpdateTrainingCommand
            {
                TrainingId = 1,
                Name = "updated",
                Description = "updated",
                Day = Day.Sunday,
                Date = new DateTime(2021, 5, 5)
            });

            response.Should().BeOfType<Response<string>>();
            response.Message.Should().Be("Training updated successfully.");
            await _trainingRepository.Received(1).UpdateAsync(Arg.Is<Training>(x => x.Information.Name == "updated" &&
            x.Day == Day.Sunday));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenTrainingWithGivenIdDoesNotExist()
        {
            var exception = await Record.ExceptionAsync(() => _sut.Handle(new UpdateTrainingCommand
            {
                TrainingId = 1
            }));

            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Training with id: '1' was not found.");
        }
    }
}
