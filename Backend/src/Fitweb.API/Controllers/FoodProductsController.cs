using Fitweb.Application.Commands.FoodProducts.Add;
using Fitweb.Application.Commands.FoodProducts.Delete;
using Fitweb.Application.Commands.FoodProducts.Update;
using Fitweb.Application.Constants;
using Fitweb.Application.Queries.FoodProduts.Get;
using Fitweb.Application.Queries.FoodProduts.GetList;
using Fitweb.Application.Requests;
using Fitweb.Domain.FoodProducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Controllers
{
    public class FoodProductsController : BaseApiController
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await Mediator.Send(new GetFoodProductQuery
            {
                Id = id
            });

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery pagination, [FromQuery] string searchName, 
            [FromQuery] FoodGroup? foodGroup)
        {
            var response = await Mediator.Send(new GetFoodProductListQuery
            {
                Pagination = pagination,
                SearchName = searchName,
                FoodGroup = foodGroup
            });

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = PolicyConstants.IsAdministratorOrIsAthlete)]
        public async Task<IActionResult> Create([FromBody] AddFoodProductCommand command)
        {
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Policy = PolicyConstants.IsAdministratorOrIsAthlete)]
        public async Task<IActionResult> Delete([FromBody] DeleteFoodProductCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        [Authorize(Policy = PolicyConstants.IsAdministratorOrIsAthlete)]
        public async Task<IActionResult> Put([FromBody] UpdateFoodProductCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
