using Fitweb.Application.Commands.AthleteFoodProducts.Add;
using Fitweb.Application.Commands.AthleteFoodProducts.Delete;
using Fitweb.Application.Commands.AthleteFoodProducts.Update;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{

    [Route("api/myfoodproducts")]
    public class AthleteFoodProductsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddAthleteFoodProductCommand command)
        {
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteAthleteFoodProductCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateAthleteFoodProductCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
