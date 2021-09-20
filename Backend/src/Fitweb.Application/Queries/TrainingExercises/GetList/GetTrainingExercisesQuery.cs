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

namespace Fitweb.Application.Queries.TrainingExercises.GetList
{
    public class GetTrainingExercisesQuery : AuthorizeRequest, IRequest<Response<TrainingExercisesWithSetsDto>>
    {
        public int TrainingId { get; set; }
    }
}
