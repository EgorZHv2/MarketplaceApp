using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class XmlSerializationException:ApiException
    {
        public XmlSerializationException():base("Ошибка при конвертации XML файла","XML serialization exception",System.Net.HttpStatusCode.InternalServerError) { }
    }
}
