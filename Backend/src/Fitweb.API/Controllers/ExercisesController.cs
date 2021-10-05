using Fitweb.Application.Queries.Exercises.GetList;
using Fitweb.Application.Requests;
using Fitweb.Domain.Exercises;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{
    public class ExercisesController : BaseApiController
    {
        /// <summary>
        /// Get exercises
        /// </summary>
        /// <param name="pagination">Pagination</param>
        /// <param name="searchName">Exercise name</param>
        /// <param name="partOfBody">Part of body</param>
        /// <returns>A list of exercises</returns>
        /// <remarks>
        /// Sample request
        /// GET /api/exercises
        /// 
        /// </remarks>
        /// <response code="200"> Returns a list of exercises</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll([FromQuery]PaginationQuery pagination, [FromQuery] string searchName,
            [FromQuery]PartOfBody? partOfBody)
        {
            var response = await Mediator.Send(new GetExercisesListQuery
            {
                Pagination = pagination,
                SearchName = searchName,
                PartOfBody = partOfBody
            });

            return Ok(response);
        }
    }
}
