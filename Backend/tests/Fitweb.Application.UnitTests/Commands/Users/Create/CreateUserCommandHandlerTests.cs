using Fitweb.Application.Commands.Users.Create;
using Fitweb.Application.Interfaces.Identity;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace Fitweb.Application.UnitTests.Commands.Users.Create
{
    public class CreateUserCommandHandlerTests
    {
        private readonly IIdentityService _identityService;
        private readonly IAthleteRepository _athleteRepository;
        private readonly RegisterUserCommandHandler _sut;

        public CreateUserCommandHandlerTests()
        {
            _identityService = Substitute.For<IIdentityService>();
            _athleteRepository = Substitute.For<IAthleteRepository>();
            _sut = new RegisterUserCommandHandler(_identityService, _athleteRepository);
        }

        [Fact]
        public async Task CreateUser_ShouldAddNewUser()
        {
            var command = new RegisterUserCommand
            {
                Username = "testUser",
                Email = "testUser@email.com",
                Password = "testSecret1="
            };

            var userId = "user_id";
            _identityService.CreateUserAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>()).Returns(userId);

            var result = await _sut.Handle(command);

            result.Should().BeOfType<Response<string>>();
            result.Message.Should().Be("User added successfully.");
        }
    }
}
