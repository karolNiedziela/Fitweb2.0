using Fitweb.Application.Responses;
using Fitweb.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Helpers
{
    public static class PaginationHelper
    {
        public static PagedResponse<T> CreatePagedResponse<T>(IEnumerable<T> pagedData, PaginationFilter pagination, int totalItems)
        {
            var response = new PagedResponse<T>(pagedData, pagination.PageNumber, pagination.PageSize);
            var totalPages = totalItems / (double)pagination.PageSize;
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            response.HasNextPage = pagination.PageNumber >= 1 && pagination.PageNumber < roundedTotalPages;
            response.HasPreviousPage = pagination.PageNumber - 1 >= 1 && pagination.PageNumber <= roundedTotalPages;
            response.TotalPages = roundedTotalPages;
            response.TotalItems = totalItems;

            return response;
        }
    }
}
