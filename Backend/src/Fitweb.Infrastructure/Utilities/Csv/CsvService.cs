using CsvHelper;
using CsvHelper.Configuration;
using Fitweb.Application.Interfaces.Utilities.Csv;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Utilities.Csv
{
    public class CsvService<T, TMap> : ICsvService<T, TMap>
        where T : class
        where TMap : ClassMap
    {
        public List<T> ReadCsvAsync(string filePath)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    MissingFieldFound = null,
                    IncludePrivateMembers = true,                
                };

                using var reader = new StreamReader(filePath);
                using var csv = new CsvReader(reader, config);
                csv.Context.RegisterClassMap<TMap>();
                var records = csv.GetRecords<T>().ToList();

                return records;
            }
            catch (UnauthorizedAccessException exception)
            {
                throw new Exception(exception.Message);
            }
            catch (FieldValidationException exception)
            {
                throw new Exception(exception.Message);
            }
            catch (CsvHelperException exception)
            {
                throw new Exception(exception.Message);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
