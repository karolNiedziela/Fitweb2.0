using Fitweb.Application.DTO;
using Fitweb.Application.Requests;
using Fitweb.Application.Responses;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Queries.Trainings.GetList
{
    public class GetTrainingsListQuery : AuthorizeRequest, IRequest<PagedResponse<TrainingDto>>
    {
        public PaginationQuery Pagination { get; set; }
    }
}
