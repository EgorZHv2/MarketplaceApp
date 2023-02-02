using System.Net;

namespace Logic.Exceptions
{
    public abstract class ApiException : Exception
    {
        public string UserMessage { get; set; }
        public string LogMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiException(string userMessage, string logMessage, HttpStatusCode statusCode)
        {
            UserMessage = userMessage;
            LogMessage = logMessage;
            StatusCode = statusCode;
        }
    }
}