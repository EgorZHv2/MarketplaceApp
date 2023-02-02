namespace WebAPi.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string recipientAdress, string subject, string text, CancellationToken cancellationToken = default);
    }
}