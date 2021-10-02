using Fitweb.Application.Commands.FoodProducts.Delete;
using Fitweb.Domain.Exceptions;
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

namespace Fitweb.Application.UnitTests.Commands.FoodProducts.Delete
{
    public class DeleteFoodProductCommandHandlerTests
    {
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly DeleteFoodProductCommandHandler _sut;

        public DeleteFoodProductCommandHandlerTests()
        {
            _foodProductRepository = Substitute.For<IFoodProductRepository>();
            _sut = new DeleteFoodProductCommandHandler(_foodProductRepository);
        }

        [Fact]
        public async Task Handle_ShouldRemoveFoodProduct_WhenFoodProductExists()
        {
            var foodProduct = FoodProductBuilder.Build(10, "Kiwi");
            _foodProductRepository.GetByIdAsync(Arg.Any<int>()).Returns(foodProduct);

            var response = await _sut.Handle(new DeleteFoodProductCommand
            {
                FoodProductId = 10
            });

            response.Message.Should().Be("Food product removed successfully.");
            await _foodProductRepository.Received(1).RemoveAsync(Arg.Is(foodProduct));
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenFoodProductDoesNotExist()
        {
            _foodProductRepository.GetByIdAsync(Arg.Any<int>()).ReturnsNull();

            var exception = await Record.ExceptionAsync(() => _sut.Handle(new DeleteFoodProductCommand
            {
                FoodProductId = 5
            }));

            exception.Should().BeOfType<NotFoundException>();
            exception.Message.Should().Be("Food product with id: '5' was not found.");
        }
    }
}
