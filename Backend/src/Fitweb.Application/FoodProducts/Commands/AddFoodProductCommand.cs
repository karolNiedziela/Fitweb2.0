using Fitweb.Application.DTO;
using Fitweb.Domain.FoodProducts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.FoodProducts.Commands
{
    public class AddFoodProductCommand : IRequest
    {
       public InformationDto Information { get; set; }

        public double Calories { get; set; }

        public NutrientDto Nutrient { get; set; }

        public FoodGroup FoodGroup { get; set; }
    }
}
