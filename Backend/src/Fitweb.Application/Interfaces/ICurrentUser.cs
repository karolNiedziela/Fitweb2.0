using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Interfaces
{
    public interface ICurrentUser
    {
        public string UserId { get; }
    }
}
