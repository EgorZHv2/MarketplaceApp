using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class PasswordGeneratorService : IPasswordGeneratorService
    {
        Random rnd = new Random();

        public string GeneratePassword()
        {
            string password = string.Empty;
            for (int i = 0; i < rnd.Next(15, 20); i++)
            {
                switch (rnd.Next(1, 4))
                {
                    case 1:
                        password += (char)rnd.Next(65, 91);
                        break;
                    case 2:
                        password += (char)rnd.Next(97, 123);
                        break;
                    case 3:
                        password += Convert.ToString(rnd.Next(0, 10));
                        break;
                }
            }

            return password;
        }
    }
}
