using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class ReviewNotFoundException:ApiException
    {
        public ReviewNotFoundException():base("Review not found", System.Net.HttpStatusCode.NotFound) { }
    }
}
