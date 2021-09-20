using FluentValidation;
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
        }
    }
}
