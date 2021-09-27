using AutoMapper;
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
    public class AddFoodProductCommandHandler : IRequestHandler<AddFoodProductCommand>
    {
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly IMapper _mapper;
        private readonly IAthleteRepository _athleteRepository;

        public AddFoodProductCommandHandler(IFoodProductRepository foodProductRepository, IMapper mapper, IAthleteRepository athleteRepository)
        {
            _foodProductRepository = foodProductRepository;
            _mapper = mapper;
            _athleteRepository = athleteRepository;
        }

        public async Task<Unit> Handle(AddFoodProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _foodProductRepository.GetByNameAsync(request.Name);
            if (product is not null)
            {
                throw new AlreadyExistsException(nameof(FoodProduct), request.Name);
            }

            var newProduct = _mapper.Map<FoodProduct>(request);
            await _foodProductRepository.AddAsync(newProduct);

            return Unit.Value;
        }
    }
}
