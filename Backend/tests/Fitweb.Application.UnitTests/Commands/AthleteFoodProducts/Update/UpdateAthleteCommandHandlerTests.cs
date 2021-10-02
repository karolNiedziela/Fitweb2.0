using Fitweb.Application.Commands.AthleteFoodProducts.Update;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.UnitTests.Builders;
using FluentAssertions;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.AthleteFoodProducts.Update
{
    public class UpdateAthleteCommandHandlerTests
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly UpdateAthleteFoodProductCommandHandler _sut;

        public UpdateAthleteCommandHandlerTests()
        {
            _athleteRepository = Substitute.For<IAthleteRepository>();
            _sut = new UpdateAthleteFoodProductCommandHandler(_athleteRepository);
        }

        [Fact]
        public async Task Handle_ReturnUpdatedResponse()
        {
            var athlete = AthleteBuilder.BuildWithFoodProduct();

            _athleteRepository.GetFoodProducts(Arg.Any<string>()).Returns(athlete);

            var result = await _sut.Handle(new UpdateAthleteFoodProductCommand
            {
                UserId = AthleteBuilder.DefaultUserId,
                AthleteFoodProductId = 1,
                Weight = 50
            });

            result.Should().BeOfType<Response<string>>();
            result.Message.Should().Be("Athlete food product updated successfully.");
            athlete.FoodProducts[0].Weight.Should().Be(50);
            await _athleteRepository.Received(1).UpdateAsync(Arg.Is(athlete));
        }
    }
}
