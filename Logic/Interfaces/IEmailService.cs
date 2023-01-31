using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPi.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string recipientAdress,string subject, string text,CancellationToken cancellationToken = default);
    }
}
