using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Filters
{
    public class PaginationFilter
    {
        public const int DefaultPageSize = 25;

        public const int MaxPageSize = 100;

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = DefaultPageSize;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? pageNumber : pageNumber;
            PageSize = pageSize > MaxPageSize 
                ? MaxPageSize 
                : pageSize < DefaultPageSize 
                    ? DefaultPageSize
                    : pageSize;
        }

    }
}
