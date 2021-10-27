using Fitweb.Application.Commands.Trainings.Delete;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes.Repositories;
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

namespace Fitweb.Application.UnitTests.Commands.Trainings.Delete
{
    public class DeleteTrainingCommandHandlerTests
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly DeleteTrainingCommandHandler _sut;

        public DeleteTrainingCommandHandlerTests()
        {
            _athleteRepository = Substitute.For<IAthleteRepository>();
            _trainingRepository = Substitute.For<ITrainingRepository>();
            _sut = new DeleteTrainingCommandHandler(_athleteRepository, _trainingRepository);
        }

        [Fact]
        public async Task Handle_ShouldRemoveTrainingWithGivenId()
        {
            var athlete = AthleteBuilder.BuildWithTrainings();
            _athleteRepository.GetTrainings(Arg.Any<string>()).Returns(athlete);

            var response = await _sut.Handle(new DeleteTrainingCommand
            {
                UserId = AthleteBuilder.DefaultUserId,
                Id = 1
            });

            response.Should().BeOfType<Response<string>>();
            response.Message.Should().Be("Training removed successfully.");
            await _trainingRepository.Received(1).RemoveAsync(Arg.Is<Training>(x => x.Id == 1));
        }
    }
}
