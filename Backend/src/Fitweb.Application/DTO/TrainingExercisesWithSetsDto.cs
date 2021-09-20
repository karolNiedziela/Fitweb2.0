using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.DTO
{
    public class TrainingExercisesWithSetsDto
    {
        public int TrainingId { get; set; }

        public List<TrainingExerciseWithSetsDto> Exercises { get; set; }
    }
}
