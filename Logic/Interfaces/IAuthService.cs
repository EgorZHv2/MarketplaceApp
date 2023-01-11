using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IAuthService
    {
        public Task VerifyEmail(string email, string code);
    }
}
