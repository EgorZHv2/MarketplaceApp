using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class MappingException : Exception
    {
        public  string ExceptionClass { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public MappingException(string message, string exceptionclass) : base(message)
        {
            ExceptionClass = exceptionclass;
            StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}
