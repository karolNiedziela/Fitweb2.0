using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.DietInformations.Update
{
    public class UpdateDietInformationCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        public int DietInformationId { get; set; }

        public double TotalCalories { get; set; }

        public double TotalProteins { get; set; }

        public double TotalCarbohydrates { get; set; }

        public double TotalFats { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
