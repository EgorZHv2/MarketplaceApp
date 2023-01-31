using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IHashService
    {
        public string HashPassword(string str);
        public bool ComparePasswordWithHash(string password, string hash);
    }
}
