using MediatR;
using VirtualClass.Application.ViewModel;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.UserCommands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public LoginUserHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputerSha256Hash(request.Password);

            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

            if (user == null) { return null!; }

            var token = _authService.GenerateTokenJwt(user.Email, user.Role.Name);

            return new LoginUserViewModel(user.FullName, user.Role.Name, user.Email, token);
        }
    }
}
