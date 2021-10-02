using Fitweb.Application.Commands.DietInformations.Delete;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.UnitTests.Builders;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.DietInformations.Delete
{
    public class DeleteDietInformationCommandHandlerTests
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly DeleteDietInformationCommandHandler _sut;

        public DeleteDietInformationCommandHandlerTests()
        {
            _athleteRepository = Substitute.For<IAthleteRepository>();
            _sut = new DeleteDietInformationCommandHandler(_athleteRepository);
        }

        [Fact]
        public async Task Handle_ShouldRemoveDietInformation()
        {
            var athlete = AthleteBuilder.Build();
            var dietInformation = new DietInformation(250, 120, 300, 40)
            {
                Id = 1
            };
            athlete.AddDietInformation(dietInformation);
            _athleteRepository.GetDietInformations(Arg.Any<string>()).Returns(athlete);

            var dietInformationCountBeforeRemoving = athlete.DietInformations.Count;

            var response = await _sut.Handle(new DeleteDietInformationCommand
            {
                DietInformationId = 1,
                UserId = AthleteBuilder.DefaultUserId
            });

            dietInformationCountBeforeRemoving.Should().Be(1);
            response.Message.Should().Be("Diet information removed successfully.");
            athlete.DietInformations.Count.Should().Be(0);
            await _athleteRepository.Received(1).RemoveDietInformation(Arg.Is(dietInformation));
        }
    }
}
