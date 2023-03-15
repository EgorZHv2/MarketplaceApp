using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class CategoryNotFoundException:ApiException
    {
        public CategoryNotFoundException():base("Category not found",System.Net.HttpStatusCode.NotFound) { }
    }
}
