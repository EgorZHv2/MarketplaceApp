using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class WrongVerificationCodeException:ApiException
    {
        public WrongVerificationCodeException() : base("Wrong verification code", System.Net.HttpStatusCode.Unauthorized) { }
    }
}
