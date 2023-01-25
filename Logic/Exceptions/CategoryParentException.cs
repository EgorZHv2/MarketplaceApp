using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class CategoryParentException:ApiException
    {
        public CategoryParentException(string userMessage, string logMessage) : base(userMessage,  logMessage,HttpStatusCode.NotFound)
        {
           
        }
    }
}
