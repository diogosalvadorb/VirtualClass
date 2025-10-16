using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.UserCommands.ForgotPassword
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, ServiceResult>
    {
        private readonly IUserRepository _repository;
        private readonly IEmailService _emailService;

        public ForgotPasswordHandler(IUserRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<ServiceResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _repository.GetUserByEmailAsync(request.Email);

                if (user == null)
                {
                    return ServiceResult.Success();
                }

                user.GeneratePasswordResetToken();
                await _repository.UpdateUserAsync(user);

                
                var emailSend= await _emailService.SendPasswordResetEmailAsync(
                    user.Email,
                    user.FullName,
                    user.PasswordResetToken!);

                if (!emailSend)
                { 
                    return ServiceResult.Error(
                        "Não foi possível enviar o email de recuperação de senha.", 
                        ErrorTypeEnum.Failure);
                }

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Error($"Erro ao processar recuperação de senha: {ex.Message}", ErrorTypeEnum.Failure);
            }
        }
    }
}
