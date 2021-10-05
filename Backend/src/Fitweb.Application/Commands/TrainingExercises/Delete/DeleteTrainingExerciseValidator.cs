using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Delete
{
    public class DeleteTrainingExerciseValidator : AbstractValidator<DeleteTrainingExerciseCommand>
    {
        public DeleteTrainingExerciseValidator()
        {
            RuleFor(x => x.TrainingId)
              .NotNull();

            RuleFor(x => x.ExerciseId)
                .NotNull();
        }
    }
}
