using System.Net;

namespace Logic.Exceptions
{
    public  class ApiException : Exception
    {
        public string UserMessage { get; set; }
        public string LogMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiException()
        {

        }
        public ApiException(string logMessage, HttpStatusCode statusCode)
        {
            LogMessage = logMessage;
            StatusCode = statusCode;
        }
        public ApiException(string logMessage,string userMessage, HttpStatusCode statusCode)
        {
            LogMessage = logMessage;
            StatusCode = statusCode;
            UserMessage= userMessage;
        }
    }
}