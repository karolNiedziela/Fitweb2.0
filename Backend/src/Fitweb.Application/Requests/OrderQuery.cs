using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Requests
{
    public class OrderQuery
    {
        public string ColumnName { get; set; }

        public bool IsAscending { get; set; } = true;
    }
}
