using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
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
    public class AddTrainingCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Day Day { get; set; } = Day.Monday;

        public DateTime? Date { get; set; } = null;
    }
}
