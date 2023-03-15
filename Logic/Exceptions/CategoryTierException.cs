using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class CategoryTierException:ApiException
    {
        public int MaxCategoryTier { get; set; }
        public CategoryTierException(int maxtier) : base("Category tier over max tier",System.Net.HttpStatusCode.BadRequest) 
        {
            MaxCategoryTier = maxtier;
        }
    }
}
