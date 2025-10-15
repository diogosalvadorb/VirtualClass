using MediatR;

namespace VirtualClass.Application.Commands.UserCommands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public ConfirmEmailCommand(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
