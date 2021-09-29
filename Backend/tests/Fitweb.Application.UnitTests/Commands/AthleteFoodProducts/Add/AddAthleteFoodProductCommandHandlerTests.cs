using Fitweb.Application.Commands.AthleteFoodProducts.Add;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.FoodProducts.Repositories;
using Fitweb.Domain.ValueObjects;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.AthleteFoodProducts.Add
{
    public class AddAthleteFoodProductCommandHandlerTests
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly AddAthleteFoodProductCommandHandler _sut;

        public AddAthleteFoodProductCommandHandlerTests()
        {
            _athleteRepository = Substitute.For<IAthleteRepository>();
            _foodProductRepository = Substitute.For<IFoodProductRepository>();
            _sut = new AddAthleteFoodProductCommandHandler(_athleteRepository, _foodProductRepository);
        }

        [Fact]
        public async Task Handle_ShouldAddAthleteFoodProduct_WhenFoodProductExists()
        {
            var athlete = new Athlete("testAthlete");
            _athleteRepository.GetFoodProducts(Arg.Any<string>()).Returns(athlete);

            var foodProduct = new FoodProduct(Information.Create("testFoodProduct", null), Calories.Create(250),
                Nutrient.Create(10, 10, 10), FoodGroup.Cereals)
            {
                Id = 1
            };
            _foodProductRepository.GetByIdAsync(Arg.Any<int>()).Returns(foodProduct);

            var result = await _sut.Handle(new AddAthleteFoodProductCommand
            {
                UserId = "testAthlete",
                FoodProductId = 1,
                Weight = 50
            });

            result.Should().BeOfType<Response<string>>();
            result.Message.Should().Be("Athlete food product added successfully.");
            athlete.FoodProducts.Count.Should().Be(1);
            await _athleteRepository.Received(1).UpdateAsync(Arg.Is(athlete));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenFoodProductDoesNotExist()
        {
            var athlete = new Athlete("testAthlete");
            _athleteRepository.GetFoodProducts(Arg.Any<string>()).Returns(athlete);

            _foodProductRepository.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

            var exception = await Record.ExceptionAsync(() => _sut.Handle(new AddAthleteFoodProductCommand
            {
                UserId = "testAthlete",
                FoodProductId = 10,
                Weight = 50
            }));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Food product with id: '10' was not found.");
        }
    }
}
