using System.Net;

namespace Marketplace.DTO
{
    public class ApiExceptionDTO
    {
        public string UserMessage { get; set; }
        public string LogMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
