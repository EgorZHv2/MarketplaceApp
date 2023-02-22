using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class WrongEmailAddressException:ApiException
    {
        public WrongEmailAddressException("Неверный Email адрес","Wrong email", HttpStatusCode.NotFound) { }
    }
}
