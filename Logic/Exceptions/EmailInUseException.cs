using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class EmailInUseException:ApiException
    {
        public EmailInUseException():base("Почта уже используется","Email already in user",System.Net.HttpStatusCode.Conflict) { }
    }

}
