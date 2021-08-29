using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Entity { get; set; }

        public string ErrorCode => string.IsNullOrEmpty(Entity) 
            ? "not_found"
            : $"{Entity}_not_found";

        public NotFoundException(object entity, int key) : base($"{entity.GetType().Name} with id: '{key}' was not found.")
        {
            Entity = entity.GetType().Name;
        }

        public NotFoundException(object entity, Guid key) : base($"{entity.GetType().Name} with id: '{key}' was not found.")
        {
            Entity = entity.GetType().Name;
        }

        public NotFoundException(object entity, string key) : base($"{entity.GetType().Name} with id: '{key}' was not found.")
        {
            Entity = entity.GetType().Name;
        }
    }
}
