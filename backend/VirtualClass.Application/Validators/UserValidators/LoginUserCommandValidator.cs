using FluentValidation;
using VirtualClass.Application.Commands.UserCommands.LoginUser;

namespace VirtualClass.Application.Validators.UserValidators
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email é obrigatório.")
                 .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Senha é obrigatória.");
        }
    }
}
