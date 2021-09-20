using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Trainings.Add
{
    public class AddTrainingValidator : AbstractValidator<AddTrainingCommand>
    {
        public AddTrainingValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(x => x.Day)
                .NotNull()
                .WithMessage("Day is required");
        }
    }
}
