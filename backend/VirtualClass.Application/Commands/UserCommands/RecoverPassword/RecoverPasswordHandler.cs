using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.UserCommands.RecoverPassword
{
    public class RecoverPasswordHandler : IRequestHandler<RecoverPasswordCommand, ServiceResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public RecoverPasswordHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<ServiceResult> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByPasswordResetTokenAsync(request.Token);

                if (user == null)
                {
                    return ServiceResult.Error("Token inválido.", ErrorTypeEnum.Validation);
                }

                if (!user.IsPasswordResetTokenValid())
                {
                    return ServiceResult.Error("Token expirado.", ErrorTypeEnum.Validation);
                }

                var newPasswordHash = _authService.ComputerSha256Hash(request.NewPassword);
                user.ResetPassword(newPasswordHash);

                await _userRepository.UpdateUserAsync(user);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Error($"Erro ao recuperar senha: {ex.Message}", ErrorTypeEnum.Failure);
            }
        }
    }
}
