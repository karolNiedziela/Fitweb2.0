using Fitweb.Application.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.DietInformations.Delete
{
    public class DeleteDietInformationCommand : AuthorizeRequest, IRequest
    {
        public int DietInformationId { get; set; }
    }
}
