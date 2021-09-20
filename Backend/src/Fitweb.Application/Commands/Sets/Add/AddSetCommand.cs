using Fitweb.Application.Requests;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.Sets.Add
{
    public class AddSetCommand : AuthorizeRequest, IRequest
    {
        [JsonIgnore]
        public int TrainingId { get; set; }

        [JsonIgnore]
        public int ExerciseId { get; set; }

        public double Weight { get; set; }

        public int NumberOfReps { get; set; }

        [DefaultValue(1)]
        public int NumberOfSets { get; set; } = 1;
    }
}
