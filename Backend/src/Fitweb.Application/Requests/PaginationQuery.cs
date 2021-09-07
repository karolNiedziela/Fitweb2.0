using Fitweb.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Requests
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = PaginationFilter.DefaultPageSize;
    }
}
