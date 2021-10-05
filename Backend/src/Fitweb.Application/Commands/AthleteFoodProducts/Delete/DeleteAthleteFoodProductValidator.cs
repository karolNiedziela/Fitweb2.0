using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.AthleteFoodProducts.Delete
{
    public class DeleteAthleteFoodProductValidator : AbstractValidator<DeleteAthleteFoodProductCommand>
    {
        public DeleteAthleteFoodProductValidator()
        {
            RuleFor(x => x.AthleteFoodProductId)
                .NotNull();
        }
    }
}
