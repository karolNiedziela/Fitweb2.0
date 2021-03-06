using Fitweb.Application.DTO;
using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Update
{
    public class UpdateTrainingExerciseCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        [JsonIgnore]
        public int TrainingId { get; set; }

        public int ExerciseId { get; set; }

        public List<SetDto> Sets { get; set; } = new();
    }
}
