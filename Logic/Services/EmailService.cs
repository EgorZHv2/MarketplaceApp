using Data.Options;
using Logic.Exceptions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using WebAPi.Interfaces;

namespace WebAPi.Services
{
    public class EmailService : IEmailService
    {
        private EmailServiceOptions _options;
        public EmailService(IOptions<EmailServiceOptions> options)
        {
            _options = options.Value;
        }
        public async void SendEmail(string recipientAdress, string subject, string text)
        {
            var emailmessage = new MimeMessage();
            emailmessage.From.Add(new MailboxAddress(_options.MailBoxName, _options.MailBoxAddress));
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
                    await client.ConnectAsync(_options.SMTPHost, _options.SMTPPort, _options.UseSSL);
                    await client.AuthenticateAsync(_options.Login, _options.Password);
                    await client.SendAsync(emailmessage);
                    await client.DisconnectAsync(true);
                }
                catch
                {
                    throw new WrongEmailAddressException();
                }
            }
        }
    }
}