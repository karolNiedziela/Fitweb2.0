using Fitweb.Application.Queries.Exercises.GetList;
using Fitweb.Application.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{
    public class ExercisesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Exercises([FromQuery]PaginationQuery pagination)
        {
            var response = await Mediator.Send(new GetExercisesListQuery
            {
                Pagination = pagination
            });

            return Ok(response);
        }
    }
}
