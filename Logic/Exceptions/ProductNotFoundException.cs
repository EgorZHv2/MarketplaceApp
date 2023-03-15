using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class ProductNotFoundException:ApiException
    {
        public ProductNotFoundException():base("Product not found", System.Net.HttpStatusCode.NotFound) { }
    }
}
