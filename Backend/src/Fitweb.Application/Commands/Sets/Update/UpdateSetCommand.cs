using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Sets.Update
{
    public class UpdateSetCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        [JsonIgnore]
        public int TrainingId { get; set; }

        [JsonIgnore]
        public int ExerciseId { get; set; }

        public int SetId { get; set; }

        public double Weight { get; set; }

        public int NumberOfReps { get; set; }

        public int NumberOfSets { get; set; }
    }
}
