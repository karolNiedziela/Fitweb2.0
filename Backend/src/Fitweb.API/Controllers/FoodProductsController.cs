using Fitweb.Application.Commands.FoodProducts.Add;
using Fitweb.Application.Commands.FoodProducts.Delete;
using Fitweb.Application.Queries.FoodProduts.Get;
using Fitweb.Application.Queries.FoodProduts.GetList;
using Fitweb.Application.Requests;
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
        public async Task<IActionResult> Details(int id)
        {
            var response = await Mediator.Send(new GetFoodProductQuery
            {
                Id = id
            });

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> FoodProducts([FromQuery] PaginationQuery pagination, [FromQuery] OrderQuery order)
        {
            var response = await Mediator.Send(new GetFoodProductListQuery
            {
                Pagination = pagination,
                Order = order
            });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddFoodProductCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteFoodProductCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
