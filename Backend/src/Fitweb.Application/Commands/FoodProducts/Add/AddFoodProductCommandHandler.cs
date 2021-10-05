using AutoMapper;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes.Repositories;
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

namespace Fitweb.Application.Commands.FoodProducts.Add
{
    public class AddFoodProductCommandHandler : IRequestHandler<AddFoodProductCommand, Response<string>>
    {
        private readonly IFoodProductRepository _foodProductRepository;

        public AddFoodProductCommandHandler(IFoodProductRepository foodProductRepository)
        {
            _foodProductRepository = foodProductRepository;
        }

        public async Task<Response<string>> Handle(AddFoodProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await _foodProductRepository.GetByNameAsync(request.Name);
            if (product is not null)
            {
                throw new AlreadyExistsException(nameof(FoodProduct), request.Name);
            }

            await _foodProductRepository.AddAsync(new FoodProduct(Information.Create(request.Name, request.Description), 
                Calories.Create(request.Calories), Nutrient.Create(request.Protein, request.Carbohydrate, request.Fat, request.SaturatedFat, request.Sugar,
                request.Fiber, request.Salt), request.FoodGroup, request.UserId));

            return Response.Added(nameof(FoodProduct));
        }
    }
}
