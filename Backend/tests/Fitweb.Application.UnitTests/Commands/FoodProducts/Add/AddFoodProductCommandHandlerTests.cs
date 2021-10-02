using AutoMapper;
using Fitweb.Application.Commands.FoodProducts.Add;
using Fitweb.Application.Mapping;
using Fitweb.Application.Responses;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.FoodProducts.Repositories;
using Fitweb.Domain.UnitTests.Builders;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Threading.Tasks;
using Xunit;

namespace Fitweb.Application.UnitTests.Commands.FoodProducts.Add
{
    public class AddFoodProductCommandHandlerTests
    {
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly IMapper _mapper;
        private readonly AddFoodProductCommandHandler _sut;

        public AddFoodProductCommandHandlerTests()
        {
            _foodProductRepository = Substitute.For<IFoodProductRepository>();
            var configurationProvider = new MapperConfiguration(configuration =>
            {
                configuration.AddMaps(typeof(FoodProductProfile).Assembly);
            });
            _mapper = configurationProvider.CreateMapper();
            _sut = new AddFoodProductCommandHandler(_foodProductRepository, _mapper);
        }

        [Theory]
        [InlineData("Product", null, 100, 20, 20, 10, null, null, null, null)]
        [InlineData("Some product", "Product description", 50, 3, 40, 0, 4, 3, 1, 1)]
        public async Task Handle_ShouldAddNewFoodProduct_WhenFoodProductWithGivenNameDoesNotExist(string name, 
            string description, double calories, double protein, double carbohydrate, double fat, 
            double? saturatedFat, double? sugar, double? fiber, double? salt)
        {
            _foodProductRepository.GetByNameAsync(Arg.Any<string>()).ReturnsNull();

            var addFoodProductCommand = new AddFoodProductCommand
            {
                Name = name,
                Description = description,
                Calories = calories,
                Protein = protein,
                Carbohydrate = carbohydrate,
                Fat = fat,
                SaturatedFat = saturatedFat,
                Fiber = fiber,
                Salt = salt,
                Sugar = sugar
            };

            var response = await _sut.Handle(addFoodProductCommand);

            response.Should().BeOfType<Response<string>>();
            response.Message.Should().Be("Food product added successfully.");
            await _foodProductRepository.Received(1).AddAsync(Arg.Is<FoodProduct>(x =>
                x.Information.Name == name &&
                x.Information.Description == description &&
                x.Calories.Value == calories &&
                x.Nutrient.Protein == protein &&
                x.Nutrient.Carbohydrate == carbohydrate &&
                x.Nutrient.Fat == fat &&
                x.Nutrient.SaturatedFat == saturatedFat &&
                x.Nutrient.Salt == salt &&
                x.Nutrient.Fiber == fiber &&
                x.Nutrient.Sugar == sugar)); ;
        }

        [Fact]
        public async Task Handle_ShouldThrowAlreadyExistsException_WhenFoodProductWithGivenNameAlreadyExists()
        {
            var foodProduct = FoodProductBuilder.Build();
            _foodProductRepository.GetByNameAsync(Arg.Any<string>()).Returns(foodProduct);

            var exception = await Record.ExceptionAsync(() => _sut.Handle(new AddFoodProductCommand
            {
                Name = FoodProductBuilder.DefaultName,
                Protein = 5,
                Carbohydrate = 10,
                Fat = 10,
                Calories = 10
            }));

            exception.Should().BeOfType<AlreadyExistsException>();
            exception.Message.Should().Be($"Food product with name: '{FoodProductBuilder.DefaultName}' already exists.");
        }
    }
}
