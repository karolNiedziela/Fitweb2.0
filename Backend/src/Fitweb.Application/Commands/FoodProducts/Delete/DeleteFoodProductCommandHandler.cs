using Fitweb.Domain.Exceptions;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.FoodProducts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.FoodProducts.Delete
{
    public class DeleteFoodProductCommandHandler : IRequestHandler<DeleteFoodProductCommand>
    {
        private readonly IFoodProductRepository _foodProductRepository;

        public DeleteFoodProductCommandHandler(IFoodProductRepository foodProductRepository)
        {
            _foodProductRepository = foodProductRepository;
        }

        public async Task<Unit> Handle(DeleteFoodProductCommand request, CancellationToken cancellationToken)
        {
            var foodProduct = await _foodProductRepository.GetByIdAsync(request.FoodProductId);
            if (foodProduct is null)
            {
                throw new NotFoundException(nameof(FoodProduct), request.FoodProductId);
            }

            await _foodProductRepository.RemoveAsync(foodProduct);

            return Unit.Value;
        }
    }
}
