using System.Net;

namespace Logic.Exceptions
{
    public class CategoryParentException : ApiException
    {
        public CategoryParentException() : base("Parent category is child category", HttpStatusCode.BadRequest)
        {
        }
    }
}