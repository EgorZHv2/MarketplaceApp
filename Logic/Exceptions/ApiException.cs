using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public abstract class ApiException:Exception
    {
        public string UserMessage { get; set; }
        public string LogMessage { get; set; }
        public DateTime DateTime { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public ApiException(string userMessage, string logMessage,HttpStatusCode statusCode)
        {
            UserMessage = userMessage;
            LogMessage = logMessage;
            DateTime = DateTime.Now;
            StatusCode = statusCode;
        }
    }
}
