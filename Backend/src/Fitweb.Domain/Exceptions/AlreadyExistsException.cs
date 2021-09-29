using Fitweb.Domain.Exceptions;
using Fitweb.Domain.Extensions;
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
            : $"{Entity.SplitByUpperCase().WhiteSpacesToUnderScore().ToLower()}_already_exists";

        public string Entity { get; set; }

        public AlreadyExistsException(string entity, string value, bool isDefaultMessage = false) 
            : base(GetMessage(entity, value, isDefaultMessage))
        {
            Entity = entity;
        }

        private static string GetMessage(string entity, string value, bool isDefaultMessage = false)
        {
            entity = entity.SplitByUpperCase();

            if (!isDefaultMessage) 
            {
                return $"{entity} with name: '{value}' already exists.";
            }

            return $"{entity} {value}";
        }
    }
}
