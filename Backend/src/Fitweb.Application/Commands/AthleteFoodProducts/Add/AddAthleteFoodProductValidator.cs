using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.AthleteFoodProducts.Add
{
    public class AddAthleteFoodProductValidator : AbstractValidator<AddAthleteFoodProductCommand>
    {
        public AddAthleteFoodProductValidator()
        {
            RuleFor(x => x.FoodProductId)
                .NotNull();

            RuleFor(x => x.Weight)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
