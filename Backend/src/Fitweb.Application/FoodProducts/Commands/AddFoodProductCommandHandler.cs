using AutoMapper;
using Fitweb.Application.Exceptions;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.FoodProducts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.FoodProducts.Commands
{
    public class AddFoodProductCommandHandler : IRequestHandler<AddFoodProductCommand>
    {
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly IMapper _mapper;

        public AddFoodProductCommandHandler(IFoodProductRepository foodProductRepository, IMapper mapper)
        {
            _foodProductRepository = foodProductRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddFoodProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _foodProductRepository.GetByNameAsync(request.Information.Name);
            if (product is not null)
            {
                throw new AlreadyExistsException(nameof(FoodProduct), request.Information.Name);
            }

            var newProduct = _mapper.Map<FoodProduct>(request);

            await _foodProductRepository.AddAsync(newProduct);

            return Unit.Value;
        }
    }
}
