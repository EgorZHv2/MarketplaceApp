using WebAPi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Services
{
    public class RandomStringGeneratorService : IRandomStringGeneratorService
    {
        Random rnd = new Random();

        public string Generate(int count)
        {
            string password = string.Empty;
            for (int i = 0; i < count; i++)
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
