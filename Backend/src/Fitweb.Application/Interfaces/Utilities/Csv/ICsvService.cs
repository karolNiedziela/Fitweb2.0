using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Interfaces.Utilities.Csv
{
    public interface ICsvService<T, TMap>
    {
        List<T> ReadCsvAsync(string filePath);
    }
}
