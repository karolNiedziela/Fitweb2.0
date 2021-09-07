using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Initializers
{
    public class SeedData : ISeedData
    {
        private readonly IEnumerable<IDataInitializer> _dataInitializers;

        public SeedData(IEnumerable<IDataInitializer> dataInitializers)
        {
            _dataInitializers = dataInitializers;
        }

        public async Task SeedAsync()
        {
            foreach (var dataInitializer in _dataInitializers)
            {
                await dataInitializer.SeedAsync();
            }
        }
    }
}
