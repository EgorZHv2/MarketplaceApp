using System.Net;

namespace Logic.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string userMessage, string logMessage) : base(userMessage, logMessage, HttpStatusCode.NotFound)
        {
        }
    }
}