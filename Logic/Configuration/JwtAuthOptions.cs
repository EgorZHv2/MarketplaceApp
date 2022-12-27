using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logic.Configuration
{
    public class JwtAuthOptions
    {
        private static readonly  string AUTH_KEY = "TQvgjeABMPOwCycOqah5EQu5yyVjpmVG";

        private static readonly SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AUTH_KEY));

        private static readonly SigningCredentials cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

        public static SymmetricSecurityKey GetKey()
        {
            return key;
        }

        public static SigningCredentials GetCredentials()
        {
            return cred;
        }
    }
}
