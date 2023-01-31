using Dadata.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;
using Logic.Interfaces;

namespace Logic.Services
{
    public class HashService:IHashService
    {
        
        public string HashPassword(string str)
        {
           
           return BCrypt.Net.BCrypt.HashPassword(str);;
           
        }

        public bool ComparePasswordWithHash(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password,hash);
        }
    }
}
