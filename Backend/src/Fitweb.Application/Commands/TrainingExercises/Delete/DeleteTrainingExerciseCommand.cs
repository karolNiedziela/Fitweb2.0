using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.TrainingExercises.Delete
{
    public class DeleteTrainingExerciseCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        public int TrainingId { get; set; }

        public int ExerciseId { get; set; }
    }
}
