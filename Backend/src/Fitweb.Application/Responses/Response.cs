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
}
