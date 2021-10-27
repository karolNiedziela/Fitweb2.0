using Fitweb.Application.Commands.Sets.Delete;
using Fitweb.Application.Responses;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.Trainings.Repositories;
using Fitweb.Domain.UnitTests.Builders;
using FluentAssertions;
using MediatR;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.Sets.Delete
{
    public class DeleteSetCommandHandlerTests
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly DeleteSetCommandHandler _sut;

        public DeleteSetCommandHandlerTests()
        {
            _trainingRepository = Substitute.For<ITrainingRepository>();
            _sut = new DeleteSetCommandHandler(_trainingRepository);
        }

        [Fact]
        public async Task Handle_ShouldDeleteSet()
        {
            var training = TrainingBuilder.BuildWithExercisesAndSets();

            _trainingRepository.GetExercisesWithSets(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(training);

            var result = await _sut.Handle(new DeleteSetCommand
            {
                TrainingId = 1,
                ExerciseId = 1,
                SetId = 2
            });

            training.Exercises.FirstOrDefault(x => x.ExerciseId == 1).Sets.Count.Should().Be(1);
            await _trainingRepository.Received(1).RemoveSet(Arg.Is<Set>(x => x.Id == 2 &&
                x.Weight == 60));
            result.Should().BeOfType<Response<string>>();
            result.Message.Should().Be("Set removed successfully.");
        }
    }
}
