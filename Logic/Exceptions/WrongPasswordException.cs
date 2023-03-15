using Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class WrongPasswordException:ApiException
    {
        public WrongPasswordException() : base("Wrong password",System.Net.HttpStatusCode.Unauthorized) { }
    }
}
