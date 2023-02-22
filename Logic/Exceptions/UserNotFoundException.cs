using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Exceptions
{
    public class UserNotFoundException:ApiException
    {
         public UserNotFoundException():base("Пользователь не найден","User not found", System.Net.HttpStatusCode.NotFound) { }
    }
}
