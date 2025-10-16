using FluentValidation;
using VirtualClass.Application.Commands.UserCommands.ForgotPassword;

namespace VirtualClass.Application.Validators.UserValidators
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");
        }
    }
}
