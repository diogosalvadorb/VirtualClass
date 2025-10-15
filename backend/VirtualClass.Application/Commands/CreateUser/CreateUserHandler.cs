using MediatR;
using VirtualClass.Application.ViewModel;
using VirtualClass.Core.Entities;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;
        public CreateUserHandler(IUserRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public async Task<CreateUserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputerSha256Hash(request.Password);

            var user = new User(request.Name, request.Email, passwordHash);

            await _repository.CreateUserAsync(user);

            return new CreateUserViewModel(user.Email);
        }
    }
}
