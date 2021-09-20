using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Fitweb.Domain.Exercises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Utilities.Csv.Converters
{
    public class StringToEnumConverter<T> : ITypeConverter where T : struct
    {
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (Enum.TryParse(text, out PartOfBody partOfBody))
            {
                return partOfBody;
            }

            return null;
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            throw new NotImplementedException();
        }
    }
}
