using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Exceptions
{
    public class AuthException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public DateTime DateTime { get; set; }
        public string? UserLogin { get; set; }


        public AuthException(string message, HttpStatusCode statusCode, string? userLogin = null ) : base(message)
        {
            StatusCode = statusCode;
           DateTime = DateTime.UtcNow;
            UserLogin = userLogin;

        }
    }
}
