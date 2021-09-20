using Fitweb.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Athletes
{
    public class DietInformation
    {
        public double TotalCalories { get; private set; }

        public double TotalProteins { get; private set; }

        public double TotalCarbohydrates { get; private set; }

        public double TotalFats { get; private set; }

        public DateTime? Beginning { get; private set; }

        public DateTime? End { get; private set; }

        public bool IsCurrent
        {
            get
            {

                //TODO: Handle It
                if (Beginning.HasValue && End.HasValue)
                {
                    return DateTime.UtcNow.IsInRange(Beginning.Value, End.Value);
                }

                return false;
            }
        }
    }
}
