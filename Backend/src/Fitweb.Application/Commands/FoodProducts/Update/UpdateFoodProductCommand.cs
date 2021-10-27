using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using Fitweb.Domain.FoodProducts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.FoodProducts.Update
{
    public class UpdateFoodProductCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Calories { get; set; }

        public double Protein { get; set; }

        public double Carbohydrate { get; set; }

        public double Fat { get; set; }

        public double? Sugar { get; set; }

        public double? SaturatedFat { get; set; }

        public double? Fiber { get; set; }

        public double? Salt { get; set; }

        public FoodGroup? FoodGroup { get; set; }
    }
}
