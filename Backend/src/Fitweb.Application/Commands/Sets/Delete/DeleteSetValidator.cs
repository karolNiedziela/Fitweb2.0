using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Sets.Delete
{
    public class DeleteSetValidator : AbstractValidator<DeleteSetCommand>
    {
        public DeleteSetValidator()
        {
            RuleFor(x => x.TrainingId)
              .NotNull();

            RuleFor(x => x.ExerciseId)
                .NotNull();

            RuleFor(x => x.SetId)
                .NotNull();
        }
    }
}
