using System.Net;

namespace Logic.Exceptions
{
    public class CategoryParentException : ApiException
    {
        public CategoryParentException(string userMessage, string logMessage) : base(userMessage, logMessage, HttpStatusCode.NotFound)
        {
        }
    }
}