using Fitweb.Application.DTO;
using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Queries.FoodProduts.GetList
{
    public class GetFoodProductListQuery : AuthorizeRequest, IRequest<PagedResponse<FoodProductDto>>
    {
        public PaginationQuery Pagination { get; set; }

        public OrderQuery Order { get; set; }
    }
}
