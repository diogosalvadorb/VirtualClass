using MediatR;
using VirtualClass.Application.ViewModel;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.UserCommands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, ServiceResult<LoginUserViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        public LoginUserHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<ServiceResult<LoginUserViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var passwordHash = _authService.ComputerSha256Hash(request.Password);
                var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

                if (user == null)
                {
                    return ServiceResult<LoginUserViewModel>.Error(
                        "Email ou senha inválidos.",
                        ErrorTypeEnum.Unauthorized);
                }

                if (!user.IsEmailConfirmed)
                {
                    return ServiceResult<LoginUserViewModel>.Error(
                        "Email não confirmado. Verifique sua caixa de entrada.",
                        ErrorTypeEnum.Unauthorized);
                }

                var token = _authService.GenerateTokenJwt(user.Email, user.Role.Name);

                return ServiceResult<LoginUserViewModel>.Success(
                    new LoginUserViewModel(user.FullName, user.Role.Name, user.Email, token));            
            }
            catch (Exception ex)
            {
                return ServiceResult<LoginUserViewModel>.Error(
                                    $"Erro ao realizar login: {ex.Message}",
                                    ErrorTypeEnum.Failure);
            }          
        }
    }
}
