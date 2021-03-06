using Fitweb.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Entity { get; set; }

        public string ErrorCode => string.IsNullOrEmpty(Entity) 
            ? "not_found"
            : $"{Entity.SplitByUpperCase().WhiteSpacesToUnderScore().ToLower()}_not_found";

        public NotFoundException(string entity, int key) : base(GetMessage(entity, key.ToString(), KeyType.Id))
        {
            Entity = entity;
        }

        public NotFoundException(string entity, string key, KeyType keyType = KeyType.Id) : base(GetMessage(entity, key, keyType))
        {                        
            Entity = entity;
        }

        private static string GetMessage(string entity, string key, KeyType keyType)
        {
            entity = entity.SplitByUpperCase();

            return keyType switch
            {
                KeyType.Id =>               $"{entity} with id: '{key}' was not found.",

                KeyType.Name =>             $"{entity} with name: '{key}' was not found.",

                KeyType.Username =>         $"{entity} with username: '{key}' was not found.",

                KeyType.Email =>            $"{entity} with email: '{key}' was not found.",

                KeyType.UserId =>           $"{entity} with user id: '{key}' was not found.",

                _ =>                        $"There was an error.",
            };
        }
    }
}
