using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Requests
{
    public class ComparisonQuery
    {
        public string ColumName { get; set; }

        public string Comparison { get; set; }

        public object Value { get; set; }
    }
}
