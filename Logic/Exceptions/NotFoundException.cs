using Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Exceptions
{
    public class NotFoundException:ApiException
    {
  
        public NotFoundException(string userMessage, string logMessage) : base(userMessage,  logMessage,HttpStatusCode.NotFound)
        {
           
        }
        
    }
}
