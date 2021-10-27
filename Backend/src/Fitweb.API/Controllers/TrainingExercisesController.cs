using Fitweb.Application.Commands.TrainingExercises.Add;
using Fitweb.Application.Commands.TrainingExercises.Delete;
using Fitweb.Application.Commands.TrainingExercises.Update;
using Fitweb.Application.Constants;
using Fitweb.Application.Queries.TrainingExercises.GetList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Fitweb.API.Controllers
{
    [Authorize(Policy = PolicyConstants.IsAthlete)]
    [Route("trainings/{tId:int}/exercises")]
    public class TrainingExercisesController : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] int tId)
        {
            var response = await Mediator.Send(new GetTrainingExercisesQuery
            {
                TrainingId = tId
            });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromRoute]int tId, [FromBody] AddTrainingExerciseCommand command)
        {
            command.TrainingId = tId;
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int tId, [FromBody] DeleteTrainingExerciseCommand command)
        {
            command.TrainingId = tId;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] int tId, [FromBody]UpdateTrainingExerciseCommand command)
        {
            command.TrainingId = tId;
            var response = await Mediator.Send(command);

            return Ok(response);
        }
    }
}
