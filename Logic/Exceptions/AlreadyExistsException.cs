using System.Net;

namespace Logic.Exceptions
{
    public class AlreadyExistsException : ApiException
    {
        public string EntityName { get; set; }
        public AlreadyExistsException(string entityname) : base(entityname + " already exists", HttpStatusCode.Conflict)
        {
            EntityName = entityname;
        }
    }
}