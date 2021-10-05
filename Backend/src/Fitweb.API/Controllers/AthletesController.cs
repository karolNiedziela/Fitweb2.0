using Fitweb.Application.Commands.Athletes.Create;
using Fitweb.Application.Commands.Athletes.Update;
using Fitweb.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{
    [Authorize]
    public class AthletesController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateAthleteCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateAthleteCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
