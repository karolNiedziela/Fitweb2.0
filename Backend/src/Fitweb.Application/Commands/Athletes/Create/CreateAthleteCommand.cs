using Fitweb.Application.Interfaces;
using Fitweb.Application.Requests;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Athletes.Create
{
    public class CreateAthleteCommand : AuthorizeRequest, IRequest
    {
        public int? Weight { get; set; }

        public int? Height { get; set; }

        public int? NumberOfTrainings { get; set; }
    }
}
