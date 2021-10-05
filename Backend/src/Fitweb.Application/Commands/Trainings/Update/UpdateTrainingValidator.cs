using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Trainings.Update
{
    public class UpdateTrainingValidator : AbstractValidator<UpdateTrainingCommand>
    {
        public UpdateTrainingValidator()
        {
            RuleFor(x => x.TrainingId)
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Day)
                .NotNull();
        }
    }
}
