
using MediatR;
using VirtualClass.Core.Repository;

namespace VirtualClass.Application.Commands.UserCommands.ConfirmEmail
{
    public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        public ConfirmEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailConfirmationTokenAsync(request.Token);

            if (user == null)
            {
                return false;
            }

            user.ConfirmEmail();

            await _userRepository.UpdateUserAsync(user);    

            return true;
        }
    }
}
