using Fitweb.Application.FoodProducts.Commands;
using Fitweb.Application.FoodProducts.Queries.GetFoodProductsList;
using Fitweb.Application.FoodProducts.Queries.GetProduct;
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
        public async Task<IActionResult> Get(int id)
        {
            var response = await Mediator.Send(new GetFoodProductQuery
            {
                Id = id
            });

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]PaginationQuery pagination)
        {
            var response = await Mediator.Send(new GetFoodProductListQuery
            {
                Pagination = pagination
            });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddFoodProductCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }
    }
}
