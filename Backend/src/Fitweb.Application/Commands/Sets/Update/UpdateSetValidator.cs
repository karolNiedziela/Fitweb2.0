using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Sets.Update
{
    public class UpdateSetValidator : AbstractValidator<UpdateSetCommand>
    {
        public UpdateSetValidator()
        {
            RuleFor(x => x.SetId)
                .NotNull();

            RuleFor(x => x.TrainingId)
               .NotNull();

            RuleFor(x => x.ExerciseId)
                .NotNull();

            RuleFor(x => x.Weight)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.NumberOfReps)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.NumberOfSets)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
