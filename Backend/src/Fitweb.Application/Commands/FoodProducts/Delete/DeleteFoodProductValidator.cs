using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.FoodProducts.Delete
{
    public class DeleteFoodProductValidator : AbstractValidator<DeleteFoodProductCommand>
    {
        public DeleteFoodProductValidator()
        {
            RuleFor(x => x.FoodProductId)
                .NotNull();
        }
    }
}
