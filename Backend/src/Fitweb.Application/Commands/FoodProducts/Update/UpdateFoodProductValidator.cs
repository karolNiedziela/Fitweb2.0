using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.FoodProducts.Update
{
    public class UpdateFoodProductValidator : AbstractValidator<UpdateFoodProductCommand>
    {
        public UpdateFoodProductValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();

            RuleFor(x => x.Name)
               .NotEmpty();

            RuleFor(x => x.Calories)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Protein)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Carbohydrate)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Fat)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Sugar)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Sugar.HasValue);

            RuleFor(x => x.SaturatedFat)
                .GreaterThanOrEqualTo(0)
                .When(x => x.SaturatedFat.HasValue);

            RuleFor(x => x.Fiber)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Fiber.HasValue);

            RuleFor(x => x.Salt)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Salt.HasValue);

            RuleFor(x => x.FoodGroup)
                .IsInEnum()
                .When(x => x.FoodGroup.HasValue);
        }
    }
}
