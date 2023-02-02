using System.Net;

namespace Logic.Exceptions
{
    public class AlreadyExistsException : ApiException
    {
        public AlreadyExistsException(string userMessage, string logMessage) : base(userMessage, logMessage, HttpStatusCode.Conflict)
        {
        }
    }
}