using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class WrongExtensionException:ApiException
    {
        public WrongExtensionException(string userMessage, string logMessage) : base(userMessage, logMessage, HttpStatusCode.UnsupportedMediaType)
        {

        }
    }
}
