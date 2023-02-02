using System.Net;

namespace Logic.Exceptions
{
    public class MappingException : ApiException
    {
        public string ExceptionClass { get; set; }

        public MappingException(Object obj) : base("Внутренняя ошибка сервера", "Error while mapping", HttpStatusCode.InternalServerError)
        {
            ExceptionClass = obj.GetType().ToString();
        }
    }
}