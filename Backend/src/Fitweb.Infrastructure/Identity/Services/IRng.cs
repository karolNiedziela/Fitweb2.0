using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Identity.Services
{
    public interface IRng
    {
        string Generate(int length = 40, bool removeSpecialChars = true);
    }
}
