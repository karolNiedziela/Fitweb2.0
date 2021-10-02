using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.DietInformations.Delete
{
    public class DeleteDietInformationCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        public int DietInformationId { get; set; }
    }
}
