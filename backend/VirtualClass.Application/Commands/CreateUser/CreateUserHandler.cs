using MediatR;
using VirtualClass.Application.ViewModel;
using VirtualClass.Core.Entities;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserViewModel>
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

        public async Task<CreateUserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputerSha256Hash(request.Password);

            var user = new User(request.FullName, request.Email, passwordHash);

            await _userRepository.CreateUserAsync(user);

            await _emailService.SendEmailConfirmationAsync(user.Email, user.FullName, user.EmailConfirmationToken!);

            return new CreateUserViewModel(user.Email);
        }
    }
}
