using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class WrongINNException:ApiException
    {
        public WrongINNException() : base("Неверный ИНН", "INN not found", System.Net.HttpStatusCode.NotFound) { }
    }
}
