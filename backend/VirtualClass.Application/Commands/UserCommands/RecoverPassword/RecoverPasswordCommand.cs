using MediatR;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.UserCommands.RecoverPassword
{
    public class RecoverPasswordCommand : IRequest<ServiceResult>
    {
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
