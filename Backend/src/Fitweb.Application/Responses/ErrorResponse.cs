using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Responses
{
    public class ErrorResponse
    {
        public string Code { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public ErrorResponse()
        {

        }

        public ErrorResponse(HttpStatusCode statusCode, List<string> errors)
        {
            StatusCode = statusCode;
            Errors = errors;
        }

        public ErrorResponse(string code, HttpStatusCode statusCode, string errorMessage)
        {
            Code = code;
            StatusCode = statusCode;
            Errors.Add(errorMessage);
        }

        public ErrorResponse(string code, HttpStatusCode statusCode, List<string> errors)
        {
            Code = code;
            StatusCode = statusCode;
            Errors = errors;
        }
    }
}
