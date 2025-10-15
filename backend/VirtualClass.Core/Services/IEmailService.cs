namespace VirtualClass.Core.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string toEmail, string userName, string confirmationToken);
        Task SendPasswordResetEmailAsync(string toEmail, string userName, string resetToken);
    }
}
