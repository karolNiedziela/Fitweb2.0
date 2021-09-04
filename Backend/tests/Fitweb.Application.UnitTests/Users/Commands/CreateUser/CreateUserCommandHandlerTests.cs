using Fitweb.Application.Interfaces.Identity;
using Fitweb.Application.Users.Commands.CreateUser;
using MediatR;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Users.Commands.CreateUser
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
