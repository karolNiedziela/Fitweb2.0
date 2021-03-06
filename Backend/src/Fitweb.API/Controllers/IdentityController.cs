using Fitweb.Application.Commands.Users.ConfirmEmail;
using Fitweb.Application.Commands.Users.Create;
using Fitweb.Application.Commands.Users.FacebookLogin;
using Fitweb.Application.Commands.Users.ForgotPassword;
using Fitweb.Application.Commands.Users.Login;
using Fitweb.Application.Commands.Users.ResendConfirmationEmail;
using Fitweb.Application.Commands.Users.ResetPassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{
    [AllowAnonymous]
    public class IdentityController : BaseApiController
    {

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand command)
        {
            await Mediator.Send(command);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginCommand command)
        {
            var token = await Mediator.Send(command);

            return Ok(token);
        }

        [HttpGet]
        [Route("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]ConfirmEmailCommand command)
        {
            if (command.Code is null || command.Email is null)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Route("resendconfirmationemail")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody]ResendConfirmationEmailCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Route("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("resetpassword")]
        public IActionResult ResetPassword([FromQuery]string email, [FromQuery]string code)
        {
            if (email is null || code is null)
            {
                return BadRequest();
            }

            return Ok(new { email, code });
        }

        [HttpPost]
        [Route("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Route("facebook")]
        public async Task<IActionResult> FacebookLogin([FromBody]FacebookLoginCommand command)
        {
            var token = await Mediator.Send(command);

            return Ok(token);
        } 
    }
}
