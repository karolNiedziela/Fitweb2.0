using Fitweb.Application.Commands.Athletes.Create;
using Fitweb.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{
    public class AthletesController : BaseApiController
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateAthleteCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }
    }
}
