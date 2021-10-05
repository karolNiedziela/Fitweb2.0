using AutoMapper;
using Fitweb.Application.DTO;
using Fitweb.Application.Helpers;
using Fitweb.Application.Responses;
using Fitweb.Domain.Filters;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.FoodProducts.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fitweb.Application.Queries.FoodProduts.GetList
{
    public class GetFoodProductsListQueryHandler
        : IRequestHandler<GetFoodProductListQuery, PagedResponse<FoodProductDto>>
    {
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly IMapper _mapper;

        public GetFoodProductsListQueryHandler(IFoodProductRepository foodProductRepository, IMapper mapper)
        {
            _foodProductRepository = foodProductRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<FoodProductDto>> Handle(GetFoodProductListQuery request,
            CancellationToken cancellationToken)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(request.Pagination);

            var (foodProducts, totalItems) = await _foodProductRepository.GetAllAsync(paginationFilter, 
                request.SearchName, request.UserId, request.FoodGroup.Value);
            var foodProductDto = _mapper.Map<List<FoodProductDto>>(foodProducts);

            return PaginationHelper.CreatePagedResponse(foodProductDto, paginationFilter, totalItems);
        }
    }
}
