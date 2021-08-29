using Fitweb.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Factories
{
    public interface IRefreshTokenFactory
    {
        RefreshToken Create(string username);
    }
}
