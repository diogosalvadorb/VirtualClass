using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using VirtualClass.Core.Services;

namespace VirtualClass.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailConfirmationAsync(string toEmail, string userName, string confirmationToken)
        {
            try
            {
                var subject = "Confirme seu email - VirtualClass";
                var confirmationLink = $"{_configuration["App:ClientUrl"]}/api/users/confirm-email?token={confirmationToken}";

                var body = $@"
                    <html>
                    <body>
                     <h2>Olá, {userName}!</h2>
                        <p>Obrigado por se registrar no VirtualClass.</p>
                        <p>Por favor, confirme seu email clicando no link abaixo:</p>
                            <p><a href='{confirmationLink}'>Confirmar Email</a></p>
                        <p>Ou copie e cole este link no seu navegador:</p>
                        <p>{confirmationLink}</p>
                        <P>{confirmationToken}</P>
                        <p>Se você não criou esta conta, por favor ignore este email.</p>
                        <br/>
                        <p>Atenciosamente,<br/>Equipe VirtualClass</p>
                    </body>
                    </html>
                ";

                await SendEmailAsync(toEmail, subject, body);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> SendPasswordResetEmailAsync(string toEmail, string userName, string resetToken)
        {
            try
            {
                var subject = "Recuperação de Senha - VirtualClass";
                var resetLink = $"{_configuration["App:ClientUrl"]}/api/users/reset-password?token={resetToken}";

                var body = $@"
                     <html>
                     <body>
                        <h2>Olá, {userName}!</h2>
                        <p>Recebemos uma solicitação para redefinir sua senha.</p>
                        <p>Clique no link abaixo para criar uma nova senha:</p>
                        <p><a href='{resetLink}'>Redefinir Senha</a></p>
                        <p>Ou copie e cole este link no seu navegador:</p>
                        <p>{resetLink}</p>
                        <P>{resetToken}</P>
                        <p>Este link expira em 24 horas.</p>
                        <p>Se você não solicitou a redefinição de senha, por favor ignore este email.</p>
                       <br/>
                        <p>Atenciosamente,<br/>Equipe VirtualClass</p>
                    </body>
                    </html>
                ";

                await SendEmailAsync(toEmail, subject, body);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpHost = _configuration["Smtp:Host"];
            var smtpPort = int.Parse(_configuration["Smtp:Port"] ?? "587");
            var smtpUser = _configuration["Smtp:User"];
            var smtpPass = _configuration["Smtp:PassWord"];
            var fromEmail = _configuration["Smtp:FromEmail"];   
            var fromName = _configuration["Smtp:FromName"] ?? "VirtualClass";   

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail!, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
        }
    }
}
