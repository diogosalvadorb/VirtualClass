using FluentValidation;
using System.Text.RegularExpressions;
using VirtualClass.Application.Commands.UserCommands.CreateUser;

namespace VirtualClass.Application.Validators.UserValidators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Nome completo é obrigatório.")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres.")
                .MaximumLength(250).WithMessage("Nome deve ter no máximo 250 caracteres.");

            RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email é obrigatório.")
                    .EmailAddress().WithMessage("Email inválido.")
                    .MaximumLength(350).WithMessage("Email deve ter no máximo 350 caracteres.");

            RuleFor(u => u.Password)
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
