using AutoMapper;
using Fitweb.Application.Commands.FoodProducts.Add;
using Fitweb.Domain.FoodProducts;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Mapping.Resolvers
{
    public class NutrientResolver : IValueResolver<AddFoodProductCommand, FoodProduct, Nutrient>
    {
        public Nutrient Resolve(AddFoodProductCommand source, FoodProduct destination, Nutrient destMember, ResolutionContext context)
        {
            return Nutrient.Create(source.Protein, source.Carbohydrate, source.Fat, source.SaturatedFat,
                source.Sugar, source.Fiber, source.Salt);
        }
    }
}
