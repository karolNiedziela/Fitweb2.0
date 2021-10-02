using AutoMapper;
using Fitweb.Application.Responses;
using Fitweb.Domain.Athletes.Repositories;
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

namespace Fitweb.Application.Commands.FoodProducts.Add
{
    public class AddFoodProductCommandHandler : IRequestHandler<AddFoodProductCommand, Response<string>>
    {
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly IMapper _mapper;

        public AddFoodProductCommandHandler(IFoodProductRepository foodProductRepository, IMapper mapper)
        {
            _foodProductRepository = foodProductRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddFoodProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await _foodProductRepository.GetByNameAsync(request.Name);
            if (product is not null)
            {
                throw new AlreadyExistsException(nameof(FoodProduct), request.Name);
            }

            var newProduct = _mapper.Map<FoodProduct>(request);
            await _foodProductRepository.AddAsync(newProduct);

            return Response.Added(nameof(FoodProduct));
        }
    }
}
