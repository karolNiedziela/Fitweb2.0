using Fitweb.Domain.Common;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Trainings;
using Fitweb.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Athletes
{
    public class Athlete : Entity
    {
        public string UserId { get; private set; }

        public int? Height { get; private set; }

        public int? Weight { get; private set; }

        // Number of training per week
        public int? NumberOfTrainings { get; private set; }

        //public List<DietInformation> DietInformation { get; private set; } = new();

        public List<Training> Trainings { get; private set; } = new();

        protected Athlete()
        {

        }

        public Athlete(string userId, int? height = null, int? weight = null, int? numberOfTrainings = null)
        {
            DomainValidator.AgainstEmptyString(userId, nameof(UserId));
            DomainValidator.AgainstNegativeNumber(height, nameof(Height));
            DomainValidator.AgainstNegativeNumber(weight, nameof(Weight));
            DomainValidator.AgainstNegativeNumber(numberOfTrainings, nameof(NumberOfTrainings));

            UserId = userId;
            Height = height;
            Weight = weight;
            NumberOfTrainings = numberOfTrainings;
        }

        public void AddTraining(Training training)
        {
            Trainings.Add(training);
        }

        public Training RemoveTraining(int trainingId)
        {
            var training = Trainings.SingleOrDefault(x => x.Id == trainingId);
            if (training is null)
            {
                throw new NotFoundException(nameof(Training), trainingId);
            }

            Trainings.Remove(training);

            return training;
        }
    }
}
