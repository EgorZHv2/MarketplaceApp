using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class TypeNotFoundException:ApiException
    {
        public TypeNotFoundException():base("Тип магазина не найден","TypeEntity not found",System.Net.HttpStatusCode.NotFound) { }
    }
}
