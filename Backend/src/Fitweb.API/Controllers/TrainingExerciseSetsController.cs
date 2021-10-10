using Fitweb.Application.Commands.Sets.Add;
using Fitweb.Application.Commands.Sets.Delete;
using Fitweb.Application.Commands.Sets.Update;
using Fitweb.Infrastructure.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{
    [Authorize(Policy = PolicyConstants.IsAthlete)]
    [Route("trainings/{tId:int}/exercises{eId:int}/sets")]
    public class TrainingExerciseSetsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddSetCommand command,
            [FromRoute] int tId, 
            [FromRoute] int eId)
        {
            command.TrainingId = tId;
            command.ExerciseId = eId;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteSetCommand command,
            [FromRoute] int tId,
            [FromRoute] int eId)
        {
            command.TrainingId = tId;
            command.ExerciseId = eId;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateSetCommand command,
            [FromRoute] int tId,
            [FromRoute] int eId)
        {
            command.TrainingId = tId;
            command.ExerciseId = eId;
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
