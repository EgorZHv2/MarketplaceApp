using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class ShopNotFoundException:ApiException
    {
         public ShopNotFoundException():base("Shop not found", System.Net.HttpStatusCode.NotFound) { }
    }
}
