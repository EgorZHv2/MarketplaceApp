using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class MappingException : ApiException
    {
        public  string ExceptionClass { get; set; }
       

        public MappingException(Object obj) :base("Внутренняя ошибка сервера","Error while mapping",HttpStatusCode.InternalServerError)
        { 
            ExceptionClass = obj.GetType().ToString();  
        }
    }
}
