using System.Net;

namespace Logic.Exceptions
{
    public class CategoryParentException : ApiException
    {
        public CategoryParentException() : base("У категории не может быть родителя являющегося её потомком", "Parent category is child category", HttpStatusCode.BadRequest)
        {
        }
    }
}