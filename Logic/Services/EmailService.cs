using Logic.Exceptions;
using MailKit.Net.Smtp;
using MimeKit;
using WebAPi.Interfaces;

namespace WebAPi.Services
{
    public class EmailService : IEmailService
    {
        public async void SendEmail(string recipientAdress, string subject, string text)
        {
            var emailmessage = new MimeMessage();
            emailmessage.From.Add(new MailboxAddress("MarketplaceApp", "appsemail@bk.ru"));
            emailmessage.To.Add(new MailboxAddress("", recipientAdress));
            emailmessage.Subject = subject;
            emailmessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.mail.ru", 465, true);
                    await client.AuthenticateAsync("appsemail@bk.ru", "UTLay0QC0x0rkJBbzbua");
                    await client.SendAsync(emailmessage);
                    await client.DisconnectAsync(true);
                }
                catch
                {
                    throw new EmailServiceException("Неверный email адрес", "Wrong email address");
                }
            }
        }
    }
}