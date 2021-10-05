using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Trainings.Delete
{
    public class DeleteTrainingValidator : AbstractValidator<DeleteTrainingCommand>
    {
        public DeleteTrainingValidator()
        {
            RuleFor(x => x.TrainingId)
                .NotNull();
        }
    }
}
