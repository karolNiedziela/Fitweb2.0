using Fitweb.Application.Commands.Athletes.Update;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Exceptions;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.Athletes.Update
{
    public class UpdateAthleteCommandHandlerTests
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly UpdateAthleteCommandHandler _sut;

        public UpdateAthleteCommandHandlerTests()
        {
            _athleteRepository = Substitute.For<IAthleteRepository>();
            _sut = new UpdateAthleteCommandHandler(_athleteRepository);
        }

        [Fact]
        public async Task Handle_ShouldUpdateAthlete_WhenAthleteExists()
        {
            var athlete = new Athlete("testUserId", 180, 65, 3);
            _athleteRepository.GetByUserId(Arg.Any<string>()).Returns(athlete);

            var result = await _sut.Handle(new UpdateAthleteCommand
            {
                UserId = athlete.UserId,
                Height = 185,
                Weight = 80,
                NumberOfTrainings = 2
            });

            athlete.Weight.Should().Be(80);
            athlete.Height.Should().Be(185);
            athlete.NumberOfTrainings.Should().Be(2);
            result.Should().BeOfType<Response<string>>();
            result.Message.Should().Be("Athlete updated successfully.");
            await _athleteRepository.Received(1).UpdateAsync(Arg.Is(athlete));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAthleteDoestNotExist()
        {
            _athleteRepository.GetByUserId(Arg.Any<string>()).ReturnsNull();

            var exception = await Record.ExceptionAsync(() =>  _sut.Handle(new UpdateAthleteCommand
            {
                UserId = "test",
            }));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Athlete with user id: 'test' was not found.");
        }
    }
}
