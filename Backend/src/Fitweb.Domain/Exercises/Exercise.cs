using Fitweb.Domain.Common;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exercises
{
    public class Exercise : Entity
    {
        public Information Information { get; private set; }

        public PartOfBody? PartOfBody { get; private set; }

        protected Exercise()
        {

        }

        public Exercise(Information information, PartOfBody? partOfBody)
        {
            Information = information;
            PartOfBody = partOfBody;
        }
    }
}
