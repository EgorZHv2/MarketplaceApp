using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Options.Сonstants
{
    public static class ApplicationConstants
    {
        public const string PasswordRegex = @"^(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])([a-zA-Z0-9!@#$%^&*]{8,16})$";
        public const string DefaultAdminGuid ="255613aa-4cd9-46aa-bccc-c443166242a9";
    }
}
