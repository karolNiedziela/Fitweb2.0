using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Athletes.Update
{
    public class UpdateAthleteCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        public int? Height { get; set; }

        public int Weight { get; set; }

        public int NumberOfTrainings { get; set; }
    }
}
