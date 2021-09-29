using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.AthleteFoodProducts.Update
{
    public class UpdateAthleteFoodProductCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        public int AthleteFoodProductId { get; set; }

        public double Weight { get; set; }
    }
}
