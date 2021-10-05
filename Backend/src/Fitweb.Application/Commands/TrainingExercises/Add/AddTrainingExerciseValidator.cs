using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Add
{
    public class AddTrainingExerciseValidator : AbstractValidator<AddTrainingExerciseCommand>
    {
        public AddTrainingExerciseValidator()
        {
            RuleFor(x => x.TrainingId)
              .NotNull();

            RuleFor(x => x.ExerciseId)
                .NotNull();
        }
    }
}
