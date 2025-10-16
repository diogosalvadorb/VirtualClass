using MediatR;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.UserCommands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ServiceResult>
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
    }
}
