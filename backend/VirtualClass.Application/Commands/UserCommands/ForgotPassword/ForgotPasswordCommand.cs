using MediatR;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.UserCommands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<ServiceResult>
    {
        public string Email { get; set; } = string.Empty;
    }
}
