using Fitweb.Domain.Trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.DTO
{
    public class TrainingDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Day { get; set; }
    }
}
