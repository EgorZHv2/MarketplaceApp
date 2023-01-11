using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IHashService
    {
        public Task<string> HashPassword(string str);
        public Task<bool> ComparePasswordWithHash(string password, string hash);
    }
}
