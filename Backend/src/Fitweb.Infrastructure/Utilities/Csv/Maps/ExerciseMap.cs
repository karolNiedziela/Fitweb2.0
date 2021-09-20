using CsvHelper.Configuration;
using Fitweb.Domain.Exercises;
using Fitweb.Infrastructure.Utilities.Csv.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Utilities.Csv.Maps
{
    public class ExerciseMap : ClassMap<Exercise>
    {
        public ExerciseMap()
        {
            Map(x => x.Information.Name).Name("Name").Index(0);
            Map(p => p.PartOfBody).Name("Part of body").Index(1).TypeConverter<StringToEnumConverter<PartOfBody>>();

            Map(p => p.Id).Ignore();
            Map(p => p.CreatedDate).Ignore();
            Map(p => p.ModifiedDate).Ignore();
            Map(p => p.Information.Description).Ignore();
        }
    }
}
