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
    public class NutrientResolver<TCommand, TEntity> : IValueResolver<TCommand, TEntity, Nutrient>
        where TCommand : class
        where TEntity : class
    {
        public Nutrient Resolve(TCommand source, TEntity destination, Nutrient destMember, ResolutionContext context)
        {
            var protein = (double)source.GetType().GetProperty("Protein").GetValue(source, null);
            var carbohydrate = (double)source.GetType().GetProperty("Carbohydrate").GetValue(source, null);
            var fat = (double)source.GetType().GetProperty("Fat").GetValue(source, null);
            var saturatedFat = (double?)source.GetType().GetProperty("SaturatedFat").GetValue(source, null);
            var sugar = (double?)source.GetType().GetProperty("Sugar").GetValue(source, null);
            var fiber = (double?)source.GetType().GetProperty("Fiber").GetValue(source, null);
            var salt = (double?)source.GetType().GetProperty("Salt").GetValue(source, null);

            return Nutrient.Create(protein, carbohydrate, fat, saturatedFat,sugar, fiber, salt);
        }
    }
}
