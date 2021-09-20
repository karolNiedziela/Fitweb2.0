using AutoMapper;
using Fitweb.Application.Requests;
using Fitweb.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Mapping
{
    public class RequestsToDomain : Profile
    {
        public RequestsToDomain()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<OrderQuery, OrderFilter>();
        }
    }
}
