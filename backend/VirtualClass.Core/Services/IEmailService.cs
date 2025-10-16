namespace VirtualClass.Core.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailConfirmationAsync(string toEmail, string userName, string confirmationToken);
        Task<bool> SendPasswordResetEmailAsync(string toEmail, string userName, string resetToken);
    }
}
