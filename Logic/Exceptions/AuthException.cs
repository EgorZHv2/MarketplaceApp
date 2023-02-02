using System.Net;

namespace Logic.Exceptions
{
    public class AuthException : ApiException
    {
        public string? UserLogin { get; set; }

        public AuthException(string userMessage, string logMessage, HttpStatusCode statusCode, string? userLogin = null) : base(userMessage, logMessage, statusCode)
        {
            UserLogin = userLogin;
        }
    }
}