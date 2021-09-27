using Fitweb.Domain.Common;
using Fitweb.Domain.Exceptions;
using Fitweb.Domain.FoodProducts;
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

        public List<DietInformation> DietInformations { get; private set; } = new();

        public List<Training> Trainings { get; private set; } = new();

        public List<AthleteFoodProduct> FoodProducts { get; private set; } = new();

        protected Athlete()
        {

        }

        public Athlete(string userId, int? height = null, int? weight = null, int? numberOfTrainings = null)
        {
            UserId = DomainValidator.AgainstEmptyString(userId, nameof(UserId));
            Height = DomainValidator.AgainstNegativeNumber(height, nameof(Height));
            Weight = DomainValidator.AgainstNegativeNumber(weight, nameof(Weight));
            NumberOfTrainings = DomainValidator.AgainstNegativeNumber(numberOfTrainings, "Number of trainings");
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

        public void AddDietInformation(DietInformation dietInformation)
        {
            // deMorgan's law + handle null (to2 === null || from1 < to2) && (to1 === null || to1 > from2)
            var existsDietInformation = DietInformations.Any(x =>
                (!dietInformation.EndDate.HasValue || x.StartDate.Value <= dietInformation.EndDate.Value) &&
                (!x.EndDate.HasValue || x.EndDate.Value >= dietInformation.StartDate.Value));

            if (existsDietInformation)
            {
                throw new AlreadyExistsException("Diet information already exists for the given time period.");
            }

            DietInformations.Add(dietInformation);
        }

        public DietInformation RemoveDietInformation(int dietInformationId)
        {
            var existingDietInformation = DietInformations.FirstOrDefault(x => x.Id == dietInformationId);
            if (existingDietInformation is null)
            {
                throw new NotFoundException("Diet information", dietInformationId);
            }

            DietInformations.Remove(existingDietInformation);

            return existingDietInformation;
        }

        public void UpdateDietInformation(DietInformation dietInformation)
        {
            var existsDietInformation = DietInformations.Where(x => x.Id != dietInformation.Id).Any(x =>
               (!dietInformation.EndDate.HasValue || x.StartDate.Value <= dietInformation.EndDate.Value) &&
               (!x.EndDate.HasValue || x.EndDate.Value >= dietInformation.StartDate.Value));

            if (existsDietInformation)
            {
                throw new AlreadyExistsException("Diet information already exists for the given time period.");
            }

            var existingDietInformation = DietInformations.FirstOrDefault(x => x.Id == dietInformation.Id);
            if (dietInformation is null)
            {
                throw new NotFoundException("Diet information", dietInformation.Id);
            }

            existingDietInformation.Update(dietInformation.TotalCalories, dietInformation.TotalProteins,
                dietInformation.TotalCarbohydrates, dietInformation.TotalFats, dietInformation.StartDate,
                dietInformation.EndDate);
        }

        public void AddFoodProduct(FoodProduct foodProduct, double weight)
        {
            if (foodProduct.UserId != null && foodProduct.UserId != UserId)
            {
                throw new NotFoundException(nameof(FoodProduct), foodProduct.Id);
            }

            FoodProducts.Add(new AthleteFoodProduct(this, foodProduct, weight));
        }
    }
}
