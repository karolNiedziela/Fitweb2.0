using Fitweb.Application.DTO;
using Fitweb.Application.Requests;
using Fitweb.Domain.FoodProducts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.FoodProducts.Add
{
    public class AddFoodProductCommand : AuthorizeRequest, IRequest
    {
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

        public int FoodGroupId { get; set; }
    }
}
