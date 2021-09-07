using AutoMapper;
using Fitweb.Application.DTO;
using Fitweb.Application.Exceptions;
using Fitweb.Application.Responses;
using Fitweb.Domain.FoodProducts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.FoodProducts.Queries.GetProduct
{
    public class GetFoodProductQueryHandler : IRequestHandler<GetFoodProductQuery, Response<FoodProductDetailsDto>>
    {
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly IMapper _mapper;

        public GetFoodProductQueryHandler(IFoodProductRepository foodProductRepository, IMapper mapper)
        {
            _foodProductRepository = foodProductRepository;
            _mapper = mapper;
        }

        public async Task<Response<FoodProductDetailsDto>> Handle(GetFoodProductQuery request, CancellationToken cancellationToken)
        {
            var foodProduct = await _foodProductRepository.GetByIdAsync(request.Id);
            if (foodProduct is null)
            {
                throw new NotFoundException("Food product", request.Id);
            }

            var foodProductDto = _mapper.Map<FoodProductDetailsDto>(foodProduct);

            return new Response<FoodProductDetailsDto>(foodProductDto);
        }
    }
}
