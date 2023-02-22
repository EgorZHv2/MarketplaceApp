using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class CategoryTierException:ApiException
    {
        public CategoryTierException(int maxtier) : base($"Максимальный уровень категории {maxtier}","Category tier over max tier",System.Net.HttpStatusCode.BadRequest) { }
    }
}
