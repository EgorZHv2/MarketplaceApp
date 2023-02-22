using System.Net;

namespace Logic.Exceptions
{
    public class WrongExtensionException : ApiException
    {
        public WrongExtensionException() : base("Неверное расширение файла", "Wrong file extension", HttpStatusCode.UnsupportedMediaType)
        {
        }
    }
}