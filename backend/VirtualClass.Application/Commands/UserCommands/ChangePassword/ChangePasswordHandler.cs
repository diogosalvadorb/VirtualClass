using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;   
        private readonly IAuthService _authService;
        public ChangePasswordHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var currentPasswordHash = _authService.ComputerSha256Hash(request.CurrentPassword);
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, currentPasswordHash);

            if (user == null)
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
