using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ServiceResult>
    {
        private readonly IUserRepository _userRepository;   
        private readonly IAuthService _authService;
        public ChangePasswordHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<ServiceResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentPasswordHash = _authService.ComputerSha256Hash(request.CurrentPassword);
                var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, currentPasswordHash);

                if (user == null)
                {
                    return ServiceResult.Error("Senha atual incorreta.", ErrorTypeEnum.Unauthorized); 
                }


                var newPasswordHash = _authService.ComputerSha256Hash(request.NewPassword);
                user.ResetPassword(newPasswordHash);

                await _userRepository.UpdateUserAsync(user);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Error($"Erro ao alterar senha: {ex.Message}", ErrorTypeEnum.Failure);
            }
            
        }
    }
}
