using Fitweb.Application.Commands.Athletes.Update;
using Fitweb.Application.Interfaces;
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
    public class AthletesController : BaseApiController
    {
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateAthleteCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
