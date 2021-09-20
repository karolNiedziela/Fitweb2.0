using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.DTO
{
    public class TrainingExerciseWithSetsDto
    {
        public int ExerciseId { get; set; }

        public string Name { get; set; }

        public List<SetDto> Sets { get; set; }
    }
}
