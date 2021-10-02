using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Commands.DietInformations.Add
{
    public class AddDietInformationCommand : AuthorizeRequest, IRequest<Response<string>>
    {
        public double TotalCalories { get; set; }

        public double TotalProteins { get; set; }

        public double TotalCarbohydrates { get; set; }

        public double TotalFats { get; set; }

        [DefaultValue(null)]
        public DateTime? StartDate { get; set; } = null;

        [DefaultValue(null)]
        public DateTime? EndDate { get; set; } = null;
    }
}
