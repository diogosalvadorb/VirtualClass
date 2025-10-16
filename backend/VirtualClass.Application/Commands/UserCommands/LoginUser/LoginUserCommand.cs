using MediatR;
using VirtualClass.Application.ViewModel;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.UserCommands.LoginUser
{
    public class LoginUserCommand : IRequest<ServiceResult<LoginUserViewModel>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
