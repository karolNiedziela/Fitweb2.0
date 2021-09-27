using Fitweb.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.AthleteFoodProducts.Add
{
    public class AddAthleteFoodProductCommand : AuthorizeRequest, IRequest
    {
        public int FoodProductId { get; set; }

        public double Weight { get; set; }
    }
}
