using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.FoodProducts.Add
{
    public class AddFoodProductValidator : AbstractValidator<AddFoodProductCommand>
    {
        public AddFoodProductValidator()
        {
            RuleFor(x => x.Information.Name)
                .NotEmpty()
                .WithMessage($"Name is required.");

            RuleFor(x => x.Calories)
                .NotNull()
                .WithMessage("Carbohydrate is required.")
                .NotEmpty()
                .WithMessage($"Calorie is required.");

            RuleFor(x => x.Nutrient.Protein)
                .NotNull()
                .WithMessage("Carbohydrate is required.")
                .NotEmpty()
                .WithMessage("Protein is required.");

            RuleFor(x => x.Nutrient.Carbohydrate)
                .NotNull()
                .WithMessage("Carbohydrate is required.")
                .NotEmpty()
                .WithMessage("Carbohydrate is required.");

            RuleFor(x => x.Nutrient.Fat)
                .NotNull()
                .WithMessage("Carbohydrate is required.")
                .NotEmpty()
                .WithMessage("Fat is required.");
        }
    }
}
