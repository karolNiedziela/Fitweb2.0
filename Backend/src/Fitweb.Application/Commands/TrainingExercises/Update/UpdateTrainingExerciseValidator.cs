using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Update
{
    public class UpdateTrainingExerciseValidator : AbstractValidator<UpdateTrainingExerciseCommand>
    {
        public UpdateTrainingExerciseValidator()
        {
            RuleFor(x => x.TrainingId)
              .NotNull();

            RuleFor(x => x.ExerciseId)
                .NotNull();
        }
    }
}
