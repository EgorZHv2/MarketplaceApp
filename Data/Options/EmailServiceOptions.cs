using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Options
{
    public class EmailServiceOptions
    {
       public string MailBoxName { get; set; }
        public string MailBoxAddress { get; set; }
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public bool UseSSL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
