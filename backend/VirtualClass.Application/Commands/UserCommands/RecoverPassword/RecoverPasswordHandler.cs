using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.UserCommands.RecoverPassword
{
    public class RecoverPasswordHandler : IRequestHandler<RecoverPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public RecoverPasswordHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<bool> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByPasswordResetTokenAsync(request.Token);

            if (user == null || !user.IsPasswordResetTokenValid())
            {
                return false;
            }

            var newPasswordHash = _authService.ComputerSha256Hash(request.NewPassword);
            user.ResetPassword(newPasswordHash);

            await _userRepository.UpdateUserAsync(user);

            return true;
        }
    }
}
