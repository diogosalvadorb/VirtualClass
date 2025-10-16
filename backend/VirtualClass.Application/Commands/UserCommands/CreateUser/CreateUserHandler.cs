using MediatR;
using VirtualClass.Application.ViewModel;
using VirtualClass.Core.Entities;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.UserCommands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ServiceResult<CreateUserViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        public CreateUserHandler(IUserRepository repository, IAuthService authService, IEmailService emailService)
        {
            _userRepository = repository;
            _authService = authService;
            _emailService = emailService;
        }

        public async Task<ServiceResult<CreateUserViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    return ServiceResult<CreateUserViewModel>.Error(
                        "Email já está em uso.",
                        ErrorTypeEnum.Validation);
                }

                var passwordHash = _authService.ComputerSha256Hash(request.Password);
                var user = new User(request.FullName, request.Email, passwordHash);

                await _userRepository.CreateUserAsync(user);

                var emailSent = await _emailService.SendEmailConfirmationAsync(user.Email, user.FullName, user.EmailConfirmationToken!);

                //emails com falha de envio enviar para uma fila de reenvio (BackGround Job)
                if (!emailSent)
                {
                    return ServiceResult<CreateUserViewModel>.Error(
                        "Usuário criado, mas não foi possível enviar o email de confirmação.",
                        ErrorTypeEnum.Failure);
                }

                return ServiceResult<CreateUserViewModel>.Success(
                    new CreateUserViewModel(user.Email));
            }
            catch (Exception ex)
            {
                return ServiceResult<CreateUserViewModel>.Error(
                    $"Erro ao criar usuário: {ex.Message}",
                    ErrorTypeEnum.Failure);
            }
        }
    }
}
