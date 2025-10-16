using MediatR;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.UserCommands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<ServiceResult>
    {
        public ConfirmEmailCommand(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
