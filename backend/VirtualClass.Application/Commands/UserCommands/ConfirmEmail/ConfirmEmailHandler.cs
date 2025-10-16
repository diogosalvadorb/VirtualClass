using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.UserCommands.ConfirmEmail
{
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, ServiceResult>
    {
        private readonly IUserRepository _userRepository;
        public ConfirmEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailConfirmationTokenAsync(request.Token);

                if (user == null)
                {
                    return ServiceResult.Error(
                        "Token inválido ou expirado.",
                        ErrorTypeEnum.Validation);
                }

                if (user.IsEmailConfirmed)
                {
                    return ServiceResult.Error(
                        "Email já foi confirmado.",
                        ErrorTypeEnum.Conflict);
                }

                user.ConfirmEmail();

                await _userRepository.UpdateUserAsync(user);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Error(
                    $"Erro ao confirmar email: {ex.Message}", 
                    ErrorTypeEnum.Failure);
            }

        }
    }
}
