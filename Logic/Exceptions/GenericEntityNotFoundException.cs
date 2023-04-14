using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class GenericEntityNotFoundException:ApiException
    {
        public GenericEntityNotFoundException():base("Entity not found",System.Net.HttpStatusCode.NotFound) { }
    }
}
