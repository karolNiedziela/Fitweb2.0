using Fitweb.Application.Commands.Users.Create;
using Fitweb.Application.Interfaces.Identity;
using MediatR;
using NSubstitute;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.Users.Create
{
    public class CreateUserCommandHandlerTests
    {
        private readonly IIdentityService _identityService;
        public CreateUserCommandHandlerTests()
        {
            _identityService = Substitute.For<IIdentityService>();
        }

        [Fact]
        public async Task CreateUser_ShouldAddNewUserWithDefaultRole()
        {
            var handler = new CreateUserCommandHandler(_identityService);

            var command = new CreateUserCommand
            {
                Username = "testUser",
                Email = "testUser@email.com",
                Password = "testSecret1="
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldBe(Unit.Value);
        }
    }
}
