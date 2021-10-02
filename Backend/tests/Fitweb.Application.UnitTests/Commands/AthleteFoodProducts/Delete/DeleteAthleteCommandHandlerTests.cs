using Fitweb.Application.Commands.AthleteFoodProducts.Delete;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.UnitTests.Builders;
using Fitweb.Domain.ValueObjects;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.AthleteFoodProducts.Delete
{
    public class DeleteAthleteCommandHandlerTests 
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly DeleteAthleteFoodProductCommandHandler _sut;

        public DeleteAthleteCommandHandlerTests()
        {
            _athleteRepository = Substitute.For<IAthleteRepository>();
            _sut = new DeleteAthleteFoodProductCommandHandler(_athleteRepository);
        }

        [Fact]
        public async Task Handle_ShouldRemoveAthleteFoodProduct()
        {
            var athlete = AthleteBuilder.BuildWithFoodProduct();

            _athleteRepository.GetFoodProducts(Arg.Any<string>()).Returns(athlete);

            var result = await _sut.Handle(new DeleteAthleteFoodProductCommand
            {
                AthleteFoodProductId = 1,
                UserId = "user_id"
            });

            result.Should().BeOfType<Response<string>>();
            result.Message.Should().Be("Athlete food product removed successfully.");
            await _athleteRepository.Received(1).UpdateAsync(Arg.Is(athlete));
        }
    }
}
