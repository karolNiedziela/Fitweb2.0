using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsInRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }

        public static bool IsInRange(this DateTime dateToCheck, DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue && !endDate.HasValue)
            {
                return true;
            }

            return dateToCheck.IsInRange(startDate.Value, endDate.Value);
        }
    }
}
