using Fitweb.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exceptions
{
    public class AlreadyExistsException : AppException
    {
        public override string ErrorCode => string.IsNullOrEmpty(Entity)
            ? "already_exists"
            : $"{Entity.ToLower()}_already_exists";

        public string Entity { get; set; }

        public AlreadyExistsException(string message) : base(message)
        {

        }

        public AlreadyExistsException(string entity, string name) : base($"{entity} with name: '{name}' already exists.")
        {
            Entity = entity;
        }
    }
}
