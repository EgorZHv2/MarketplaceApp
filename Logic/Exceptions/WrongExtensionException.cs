using System.Net;

namespace Logic.Exceptions
{
    public class WrongExtensionException : ApiException
    {
        public WrongExtensionException(string userMessage, string logMessage) : base(userMessage, logMessage, HttpStatusCode.UnsupportedMediaType)
        {
        }
    }
}