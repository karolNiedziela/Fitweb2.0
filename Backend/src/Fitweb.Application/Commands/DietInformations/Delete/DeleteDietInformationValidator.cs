using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.DietInformations.Delete
{
    public class DeleteDietInformationValidator : AbstractValidator<DeleteDietInformationCommand>
    {
        public DeleteDietInformationValidator()
        {
            RuleFor(x => x.DietInformationId)
                .NotNull();
        }
    }
}
