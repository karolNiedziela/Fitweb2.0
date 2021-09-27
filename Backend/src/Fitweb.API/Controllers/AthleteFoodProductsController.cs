using Fitweb.Application.Commands.AthleteFoodProducts.Add;
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
            await Mediator.Send(command);

            return Ok();
        }
    }
}
