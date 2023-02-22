using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class IncorrectJWTException:ApiException
    {
        public IncorrectJWTException():base("Некорректный токен авторизацияя","Incorrect JWT",System.Net.HttpStatusCode.Forbidden) { }
    }
}
