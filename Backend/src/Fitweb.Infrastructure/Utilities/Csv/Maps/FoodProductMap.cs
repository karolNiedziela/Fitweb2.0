﻿using CsvHelper;
using CsvHelper.Configuration;
using Fitweb.Domain.FoodProducts;
using Fitweb.Infrastructure.Utilities.Csv.Converters;
using System;

namespace Fitweb.Infrastructure.Utilities.Csv.Maps
{
    public class FoodProductMap : ClassMap<FoodProduct>
    {
        public FoodProductMap()
        {
      
            //Map(p => p.Name).Name("Name");
            //Map(p => p.Calories).Name("Calories");
            //Map(p => p.Protein).Name("Protein");
            //Map(p => p.Fat).Name("Fat");
            //Map(p => p.Carbohydrate).Name("Carbohydrate");
            //Map(p => p.Group).Name("Group");

            Map(p => p.Information.Name).Name("Name").Index(0);
            Map(p => p.Calories.Value).Name("Calories").Index(1).Default(0);
            Map(p => p.Nutrient.Protein).Name("Protein").Index(2).Default(0);
            Map(p => p.Nutrient.Fat).Name("Fat").Index(3).Default(0);
            Map(p => p.Nutrient.Carbohydrate).Name("Carbohydrate").Index(4).Default(0);
            Map(p => p.Group).Name("Group").Index(5).TypeConverter<OurEnumConverter<FoodGroup>>();

            Map(p => p.Id).Ignore();
            Map(p => p.CreatedDate).Ignore();
            Map(p => p.ModifiedDate).Ignore();
            Map(p => p.Information.Description).Ignore();
            Map(p => p.Nutrient.Fiber).Ignore();
            Map(p => p.Nutrient.Salt).Ignore();
            Map(p => p.Nutrient.SaturatedFat).Ignore();
            Map(p => p.Nutrient.Sugar).Ignore();
        }
    }
}