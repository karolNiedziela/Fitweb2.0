using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Persistence.Initializers
{
    public delegate IDataInitializer ServiceResolver(string key);

    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}
