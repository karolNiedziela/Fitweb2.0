using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.DTO
{
    public class TrainingExercisesDto
    {
        public int TrainingId { get; set; }

        public List<ExerciseDto> Exercises { get; set; }
    }
}
