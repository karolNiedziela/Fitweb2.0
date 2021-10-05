﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Athletes.Create
{
    public class CreateAthleteValidator : AbstractValidator<CreateAthleteCommand>
    {
        public CreateAthleteValidator()
        {
            RuleFor(x => x.Weight)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Weight.HasValue);

            RuleFor(x => x.Height)
                .GreaterThanOrEqualTo(0)
                .When(x => x.Height.HasValue);

            RuleFor(x => x.NumberOfTrainings)
                .GreaterThanOrEqualTo(0)
                .When(x => x.NumberOfTrainings.HasValue);
        }
    }
}
