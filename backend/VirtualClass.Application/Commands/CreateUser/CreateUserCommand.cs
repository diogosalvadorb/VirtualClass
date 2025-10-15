using MediatR;
using VirtualClass.Application.ViewModel;

namespace VirtualClass.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserViewModel>
    {
        public CreateUserCommand(string name, string email, string password)
        {
            FullName = name;
            Email = email;
            Password = password;
        }

        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
    }
}
