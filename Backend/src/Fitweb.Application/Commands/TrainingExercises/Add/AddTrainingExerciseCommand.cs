using Fitweb.Application.Requests;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Add
{
    public class AddTrainingExerciseCommand : AuthorizeRequest, IRequest
    {
        [JsonIgnore]
        public int TrainingId { get; set; }

        public int ExerciseId { get; set; }
    }
}
