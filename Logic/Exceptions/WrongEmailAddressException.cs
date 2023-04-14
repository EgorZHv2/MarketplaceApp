
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
        public WrongEmailAddressException():base("Wrong Email address",HttpStatusCode.NotFound) { }
    }
}
