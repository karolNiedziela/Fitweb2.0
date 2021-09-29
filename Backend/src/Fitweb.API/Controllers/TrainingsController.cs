using Fitweb.Application.Commands.Trainings.Add;
using Fitweb.Application.Commands.Trainings.Delete;
using Fitweb.Application.Queries.Trainings.GetList;
using Fitweb.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{
    [Authorize]
    public class TrainingsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Trainings([FromQuery]PaginationQuery pagination)
        {
            var response = await Mediator.Send(new GetTrainingsListQuery
            {
                Pagination = pagination
            });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddTrainingCommand command)
        {
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]DeleteTrainingCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
