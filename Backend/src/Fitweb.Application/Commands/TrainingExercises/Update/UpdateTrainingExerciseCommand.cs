using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Update
{
    public class UpdateTrainingExerciseCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        [JsonIgnore]
        public int TrainingId { get; set; }

        public int ExerciseId { get; set; }

        public int NewExerciseId { get; set; }

    }
}
