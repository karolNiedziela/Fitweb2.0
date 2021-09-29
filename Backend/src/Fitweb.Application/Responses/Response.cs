using Fitweb.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public Response()
        {

        }

        public Response(T response, string message = null)
        {
            Data = response;
            Message = message;
        }
    }

    public static class Response
    {
        public static Response<string> Added(string entity) => new(null, GetMessage(entity, ResponseType.Added));

        public static Response<string> Deleted(string entity) => new(null, GetMessage(entity, ResponseType.Deleted));

        public static Response<string> Updated(string entity) => new(null, GetMessage(entity, ResponseType.Updated));

        private static string GetMessage(string entity, ResponseType responseType)
        {
            entity = entity.SplitByUpperCase();

            return responseType switch
            {
                ResponseType.Added => $"{entity} added successfully.",

                ResponseType.Deleted => $"{entity} removed successfully.",

                ResponseType.Updated => $"{entity} updated successfully.",

                _ => ""
            };
        }
    }

    public enum ResponseType
    {
        Added = 1,
        Deleted = 2,
        Updated = 3,
    }
}
