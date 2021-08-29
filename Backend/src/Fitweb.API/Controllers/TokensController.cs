using Fitweb.Application.Tokens.Commands.RevokeToken;
using Fitweb.Application.Tokens.Commands.UseToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{
    [Authorize]
    public class TokensController : BaseApiController
    {
        [HttpPost("use")]
        public async Task<IActionResult> Use([FromBody] UseTokenCommand command)
        {
            var token = await Mediator.Send(command);

            return Ok(token);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RevokeTokenCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

    }
}
