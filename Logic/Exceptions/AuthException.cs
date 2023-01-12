using Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class AuthException : ApiException
    {
      
        public string? UserLogin { get; set; }


        public AuthException(string userMessage, string logMessage,HttpStatusCode statusCode, string? userLogin = null) : base(userMessage,  logMessage, statusCode)
        {

            UserLogin = userLogin;
        }
    }
}
