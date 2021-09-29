using Fitweb.Application.Commands.Athletes.Create;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Exceptions;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.Athletes.Add
{
    public class CreateAthleteCommandHandlerTests
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly CreateAthleteCommandHandler _sut;

        public CreateAthleteCommandHandlerTests()
        {
            _athleteRepository = Substitute.For<IAthleteRepository>();
            _sut = new CreateAthleteCommandHandler(_athleteRepository);
        }

        [Fact]
        public async Task Handle_ShouldCreateNewAthlete_WhenAthleteDoesNotExist()
        {
            _athleteRepository.GetByUserId(Arg.Any<string>()).ReturnsNull();

            var result = await _sut.Handle(new CreateAthleteCommand
            {
                UserId = "testUserId",
                Weight = 80,
                Height = 180,
                NumberOfTrainings = 2
            });

            result.Should().BeOfType<Response<string>>();
            result.Message.Should().Be("Athlete added successfully.");
            await _athleteRepository.Received(1).AddAsync(Arg.Is<Athlete>(x =>
            x.UserId == "testUserId" &&
            x.Weight == 80 &&
            x.Height == 180 &&
            x.NumberOfTrainings == 2));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAthleteAlreadyExists()
        {
            var athlete = new Athlete("testUser");

            _athleteRepository.GetByUserId(Arg.Any<string>()).Returns(athlete);

            var exception = await Record.ExceptionAsync(() => _sut.Handle(new CreateAthleteCommand
            {
                UserId = "testUser"
            }));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<AlreadyExistsException>();
            exception.Message.Should().Be("Athlete with userId: 'testUser' already exists.");
        }
    }
}
