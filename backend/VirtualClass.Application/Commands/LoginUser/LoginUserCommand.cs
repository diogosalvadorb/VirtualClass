using MediatR;
using VirtualClass.Application.ViewModel;

namespace VirtualClass.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
