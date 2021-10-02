using AutoFixture;
using AutoMapper;
using Fitweb.Application.Commands.FoodProducts.Update;
using Fitweb.Application.Mapping;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.FoodProducts.Repositories;
using Fitweb.Domain.UnitTests.Builders;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.FoodProducts.Update
{
    public class UpdateFoodProductCommandHandlerTests
    {
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly IMapper _mapper;
        private readonly UpdateFoodProductCommandHandler _sut;

        public UpdateFoodProductCommandHandlerTests()
        {
            _foodProductRepository = Substitute.For<IFoodProductRepository>();
            var configurationProvider = new MapperConfiguration(configuration =>
            {
                configuration.AddMaps(typeof(FoodProductProfile).Assembly);
            });
            _mapper = configurationProvider.CreateMapper();
            _sut = new UpdateFoodProductCommandHandler(_foodProductRepository, _mapper);
        }

        [Theory]
        [InlineData("Banana", null, 150, 2, 35, 0, null, null, null, null)]
        [InlineData("Kiwi", "just fruit", 50, 4, 21, 3, 0.5, 0.2, 1, 0.7)]
        public async Task Handle_ShouldUpdateFoodProduct_WhenFoodProductExists(string name, string description,
            double calories, double protein, double carbohydrate, double fat, double? saturatedFat, double? fiber,
            double? salt, double? sugar)
        {
            var foodProduct = FoodProductBuilder.Build();

            _foodProductRepository.GetByIdAsync(Arg.Any<int>()).Returns(foodProduct);

            var response = await _sut.Handle(new UpdateFoodProductCommand
            {
                Name = name,
                Description = description,
                Calories = calories,
                Protein = protein,
                Carbohydrate = carbohydrate,
                Fat = fat,
                SaturatedFat = saturatedFat,
                Salt = salt,
                Fiber = fiber,
                Sugar = sugar
            });

            response.Message.Should().Be("Food product updated successfully.");
            await _foodProductRepository.Received(1).UpdateAsync(Arg.Is<FoodProduct>(x => 
                x.Information.Name == name &&
                x.Information.Description == description &&
                x.Nutrient.Protein == protein &&
                x.Nutrient.Carbohydrate == carbohydrate &&
                x.Nutrient.Fat == fat &&
                x.Nutrient.SaturatedFat == saturatedFat &&
                x.Nutrient.Fiber == fiber &&
                x.Nutrient.Salt == salt &&
                x.Nutrient.Sugar == sugar));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            _foodProductRepository.GetByIdAsync(Arg.Any<int>()).ReturnsNull();
            var fixture = new Fixture();
            var updateFoodProductCommand = fixture.Create<UpdateFoodProductCommand>();

            var exception = await Record.ExceptionAsync(() => _sut.Handle(updateFoodProductCommand));

            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be($"Food product with id: '{updateFoodProductCommand.Id}' was not found.");
        }
    }
}
