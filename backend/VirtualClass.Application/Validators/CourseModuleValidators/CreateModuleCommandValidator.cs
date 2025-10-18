using FluentValidation;
using VirtualClass.Application.Commands.CourseModuleCommands.CreateCourseModule;

namespace VirtualClass.Application.Validators.CourseModuleValidators
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateCourseModuleCommand>
    {
        public CreateModuleCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Título é obrigatório.")
                .MinimumLength(3).WithMessage("Título deve ter no mínimo 3 caracteres.")
                .MaximumLength(200).WithMessage("Título deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Descrição é obrigatória.")
                .MinimumLength(10).WithMessage("Descrição deve ter no mínimo 10 caracteres.")
                .MaximumLength(1000).WithMessage("Descrição deve ter no máximo 1000 caracteres.");

            RuleFor(x => x.Order)
                .GreaterThan(0).WithMessage("Ordem deve ser maior que zero.");

            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("ID do curso é obrigatório.");
        }
    }
}
