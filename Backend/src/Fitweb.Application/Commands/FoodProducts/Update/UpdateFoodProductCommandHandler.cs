using AutoMapper;
using Fitweb.Application.Responses;
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

namespace Fitweb.Application.Commands.FoodProducts.Update
{
    public class UpdateFoodProductCommandHandler : IRequestHandler<UpdateFoodProductCommand, Response<string>>
    {
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly IMapper _mapper;

        public UpdateFoodProductCommandHandler(IFoodProductRepository foodProductRepository, IMapper mapper)
        {
            _foodProductRepository = foodProductRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateFoodProductCommand request, CancellationToken cancellationToken = default)
        {
            var foodProduct = await _foodProductRepository.GetByIdAsync(request.Id);
            if (foodProduct is null)
            {
                throw new NotFoundException(nameof(FoodProduct), request.Id);
            }

            var updatedFoodProduct = _mapper.Map<FoodProduct>(request);
            foodProduct.Update(updatedFoodProduct);

            await _foodProductRepository.UpdateAsync(foodProduct);

            return Response.Updated(nameof(FoodProduct));
        }
    }
}
