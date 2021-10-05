using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.DietInformations.Add
{
    public class AddDietInformationValidator : AbstractValidator<AddDietInformationCommand>
    {
        public AddDietInformationValidator()
        {
            RuleFor(x => x.TotalCalories)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.TotalProteins)
               .NotNull()
               .GreaterThan(0);

            RuleFor(x => x.TotalCarbohydrates)
               .NotNull()
               .GreaterThan(0);

            RuleFor(x => x.TotalFats)
               .NotNull()
               .GreaterThan(0);

            RuleFor(x => new { x.StartDate, x.EndDate })
               .Must(x => x.StartDate < x.EndDate)
               .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
               .WithMessage("End date must be greater than start date.");
        }
    }
}
