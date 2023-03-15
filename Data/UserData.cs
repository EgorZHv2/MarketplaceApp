using Data.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserData:IUserData
    {
        public CultureInfo CultureInfo { get;set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
       
    }
}
