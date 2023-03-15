using System.Net;

namespace Logic.Exceptions
{
    public class WrongExtensionException : ApiException
    {
        public WrongExtensionException() : base("Wrong file extension", HttpStatusCode.UnsupportedMediaType)
        {
        }
    }
}