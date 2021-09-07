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
            DomainValidator.AgainstNegativeNumber(protein, "Protein");
            DomainValidator.AgainstNegativeNumber(carbohydrate, "Carbohydrate");
            DomainValidator.AgainstNegativeNumber(fat, "Fat");
            if (saturatedFat.HasValue) 
                DomainValidator.AgainstNegativeNumber(saturatedFat.Value, "Saturated fat");
            if (sugar.HasValue)
                DomainValidator.AgainstNegativeNumber(sugar.Value, "Sugar");
            if (fiber.HasValue)
                DomainValidator.AgainstNegativeNumber(fiber.Value, "Fiber");
            if (salt.HasValue)
                DomainValidator.AgainstNegativeNumber(salt.Value, "Salt");

            Protein = protein;
            Carbohydrate = carbohydrate;
            Sugar = sugar;
            Fat = fat;
            SaturatedFat = saturatedFat;
            Fiber = fiber;
            Salt = salt;
        }

        public static Nutrient Create(double protein, double carbohydrate, double fat, double? saturatedFat = null,
            double? sugar = null, double? fiber = null, double? salt = null)
            => new(protein, carbohydrate, fat, saturatedFat, sugar, fiber, salt);
    }
}
