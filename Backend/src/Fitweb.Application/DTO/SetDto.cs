using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.DTO
{
    public class SetDto
    {
        public int SetId { get; set; }

        public double Weight { get; set; }

        public int NumberOfReps { get; set; }

        public int NumberOfSets { get; set; }
    }
}
