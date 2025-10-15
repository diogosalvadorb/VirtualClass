namespace VirtualClass.Core.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string toEmail, string userName, string confirmationToken);
    }
}
