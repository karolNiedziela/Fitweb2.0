using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes;
using Fitweb.Domain.Athletes.Repositories;
using Fitweb.Domain.Common;
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

namespace Fitweb.Application.Commands.AthleteFoodProducts.Add
{
    public class AddAthleteFoodProductCommandHandler : IRequestHandler<AddAthleteFoodProductCommand, Response<string>>
    {
        private readonly IAthleteRepository _athleteRepository;
        private readonly IFoodProductRepository _foodProductRepository;

        public AddAthleteFoodProductCommandHandler(IAthleteRepository athleteRepository, IFoodProductRepository foodProductRepository)
        {
            _athleteRepository = athleteRepository;
            _foodProductRepository = foodProductRepository;
        }

        public async Task<Response<string>> Handle(AddAthleteFoodProductCommand request, CancellationToken cancellationToken = default)
        {
            var athlete = await _athleteRepository.GetFoodProducts(request.UserId);

            var foodProduct = await _foodProductRepository.GetByIdAsync(request.FoodProductId);
            if (foodProduct is null)
            {
                throw new NotFoundException(nameof(FoodProduct), request.FoodProductId);
            }

            athlete.AddFoodProduct(foodProduct, request.Weight);

            await _athleteRepository.UpdateAsync(athlete);

            return Response.Added(nameof(AthleteFoodProduct));
        }
    }
}
