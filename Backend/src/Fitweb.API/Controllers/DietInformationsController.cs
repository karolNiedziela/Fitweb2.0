using Fitweb.Application.Commands.DietInformations.Add;
using Fitweb.Application.Commands.DietInformations.Delete;
using Fitweb.Application.Commands.DietInformations.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{
    [Authorize]
    public class DietInformationsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddDietInformationCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteDietInformationCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateDietInformationCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
