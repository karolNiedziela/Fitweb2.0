using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Initializers
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}
