using Fitweb.Application.Requests;
using Fitweb.Domain.Trainings;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Trainings.Add
{
    public class AddTrainingCommand : AuthorizeRequest, IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int DayId = 1;
    }
}
