using Fitweb.Application.DTO;
using Fitweb.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.FoodProducts.Queries.GetProduct
{
    public class GetFoodProductQuery : IRequest<Response<FoodProductDetailsDto>>
    {
        public int Id { get; set; }
    }
}
