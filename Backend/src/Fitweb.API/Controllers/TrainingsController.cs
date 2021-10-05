using Fitweb.Application.Commands.Trainings.Add;
using Fitweb.Application.Commands.Trainings.Delete;
using Fitweb.Application.Commands.Trainings.Update;
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
        public async Task<IActionResult> GetAll([FromQuery]PaginationQuery pagination, [FromQuery]DateTime? date = null)
        {
            var response = await Mediator.Send(new GetTrainingsListQuery
            {
                Pagination = pagination,
                Date = date
            });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddTrainingCommand command)
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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateTrainingCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
