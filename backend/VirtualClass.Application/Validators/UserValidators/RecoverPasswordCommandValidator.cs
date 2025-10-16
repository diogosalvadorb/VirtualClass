using FluentValidation;
using System.Text.RegularExpressions;
using VirtualClass.Application.Commands.UserCommands.RecoverPassword;

namespace VirtualClass.Application.Validators.UserValidators
{
    public class RecoverPasswordCommandValidator : AbstractValidator<RecoverPasswordCommand>
    {
        public RecoverPasswordCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token é obrigatório.");

            RuleFor(u => u.NewPassword)
                .NotEmpty().WithMessage("Senha inválida. A senha é obrigatória.")
                .Must(ValidPassword).WithMessage("A senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma letra minúscula e um caractere especial.");
        }

        public bool ValidPassword(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

            return regex.IsMatch(password);
        }
    }
}
