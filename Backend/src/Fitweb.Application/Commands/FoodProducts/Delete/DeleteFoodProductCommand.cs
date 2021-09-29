using Fitweb.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.FoodProducts.Delete
{
    public class DeleteFoodProductCommand : IRequest<Response<string>>
    {
        public int FoodProductId { get; set; }
    }
}
