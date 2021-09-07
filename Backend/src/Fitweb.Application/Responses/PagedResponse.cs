using Fitweb.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Responses
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPreviousPage { get; set; }

        public PagedResponse()
        {

        }

        public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
