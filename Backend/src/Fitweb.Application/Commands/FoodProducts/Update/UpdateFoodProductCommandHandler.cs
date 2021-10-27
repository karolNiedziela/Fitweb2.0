using AutoMapper;
using Fitweb.Application.Responses;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.FoodProducts.Repositories;
using Fitweb.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.FoodProducts.Update
{
    public class UpdateFoodProductCommandHandler : IRequestHandler<UpdateFoodProductCommand, Response<string>>
    {
        private readonly IFoodProductRepository _foodProductRepository;


        public UpdateFoodProductCommandHandler(IFoodProductRepository foodProductRepository)
        {
            _foodProductRepository = foodProductRepository;
        }

        public async Task<Response<string>> Handle(UpdateFoodProductCommand request, CancellationToken cancellationToken = default)
        {
            var foodProduct = await _foodProductRepository.GetByIdAsync(request.Id);
            if (foodProduct is null)
            {
                throw new NotFoundException(nameof(FoodProduct), request.Id);
            }

            if (!string.IsNullOrEmpty(foodProduct.UserId) && foodProduct.UserId != request.UserId && request.IsAdmin == false)
            {
                throw new ForbiddenOperationException();
            }

            foodProduct.Update(new FoodProduct(Information.Create(request.Name, request.Description),
                Calories.Create(request.Calories), Nutrient.Create(request.Protein, request.Carbohydrate, request.Fat, request.SaturatedFat, request.Sugar,
                request.Fiber, request.Salt), request.FoodGroup));

            await _foodProductRepository.UpdateAsync(foodProduct);

            return Response.Updated(nameof(FoodProduct));
        }
    }
}
