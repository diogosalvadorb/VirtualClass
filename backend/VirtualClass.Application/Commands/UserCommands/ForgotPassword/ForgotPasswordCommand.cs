using MediatR;

namespace VirtualClass.Application.Commands.UserCommands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
    }
}
