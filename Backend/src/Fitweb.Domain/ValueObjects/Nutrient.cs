using Fitweb.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.ValueObjects
{
    public record Nutrient
    {
        public double Protein { get; init; }

        public double Carbohydrate { get; init; }

        public double? Sugar { get; init; }

        public double Fat { get; init; }

        public double? SaturatedFat { get; init; }

        public double? Fiber { get; init; }

        public double? Salt { get; init; }

        protected Nutrient()
        {

        }

        private Nutrient(double protein, double carbohydrate, double fat, double? saturatedFat = null,
            double? sugar = null, double? fiber = null, double? salt = null)
        {
            Protein = DomainValidator.AgainstNegativeNumber(protein, "Protein");
            Carbohydrate = DomainValidator.AgainstNegativeNumber(carbohydrate, "Carbohydrate");
            Fat = DomainValidator.AgainstNegativeNumber(fat, "Fat");
            SaturatedFat = DomainValidator.AgainstNegativeNumber(saturatedFat, "Saturated fat");
            Sugar = DomainValidator.AgainstNegativeNumber(sugar, "Sugar");
            Fiber = DomainValidator.AgainstNegativeNumber(fiber, "Fiber");
            Salt = DomainValidator.AgainstNegativeNumber(salt, "Salt");
        }

        public static Nutrient Update(Nutrient nutrient)
        {
            return nutrient with
            {
                Protein = nutrient.Protein,
                Carbohydrate = nutrient.Carbohydrate,
                Fat = nutrient.Fat,
                SaturatedFat = nutrient.SaturatedFat,
                Sugar = nutrient.Sugar,
                Fiber = nutrient.Fiber,
                Salt = nutrient.Salt
            };
        }

        public static Nutrient Create(double protein, double carbohydrate, double fat, double? saturatedFat = null,
            double? sugar = null, double? fiber = null, double? salt = null)
            => new(protein, carbohydrate, fat, saturatedFat, sugar, fiber, salt);
    }
}
