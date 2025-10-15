using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Services;

namespace VirtualClass.Application.Commands.UserCommands.ForgotPassword
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IUserRepository _repository;
        private readonly IEmailService _emailService;

        public ForgotPasswordHandler(IUserRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {              
                return true;
            }

            user.GeneratePasswordResetToken();
            await _repository.UpdateUserAsync(user);

            await _emailService.SendPasswordResetEmailAsync(
                user.Email,
                user.FullName,
                user.PasswordResetToken!
            );

            return true;
        }
    }
}
