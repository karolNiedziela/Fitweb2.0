using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Filters
{
    public class OrderFilter
    {
        public string ColumnName { get; set; }

        public bool IsAscending { get; set; }

        public OrderFilter(string columnName, bool isAscending = true)
        {
            ColumnName = string.IsNullOrEmpty(columnName) ? null : columnName.ToLower();
            IsAscending = isAscending;
        }
    }
}
