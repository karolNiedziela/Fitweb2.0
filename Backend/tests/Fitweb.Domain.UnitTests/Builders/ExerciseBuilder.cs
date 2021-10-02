using Fitweb.Domain.Exercises;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.UnitTests.Builders
{
    public class ExerciseBuilder
    {
        public static Exercise Build(string name = "test_exercise", string description = "test_description",
            PartOfBody partOfBody = PartOfBody.Biceps)
        {
            return new Exercise(Information.Create(name, description), partOfBody);
        }
    }
}
