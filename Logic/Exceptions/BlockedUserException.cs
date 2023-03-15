using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class BlockedUserException:ApiException
    {
        public BlockedUserException():base("User blocked", System.Net.HttpStatusCode.Forbidden) { }
    }
}
