using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.AthleteFoodProducts.Update
{
    public class UpdateAthleteFoodProductValidator : AbstractValidator<UpdateAthleteFoodProductCommand>
    {
        public UpdateAthleteFoodProductValidator()
        {
            RuleFor(x => x.AthleteFoodProductId)
             .NotNull();

            RuleFor(x => x.Weight)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
