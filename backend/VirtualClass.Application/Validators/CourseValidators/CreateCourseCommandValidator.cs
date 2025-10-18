using FluentValidation;
using VirtualClass.Application.Commands.CourseCommands.CreateCourse;

namespace VirtualClass.Application.Validators.CourseValidators
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Título é obrigatório.")
                .MinimumLength(3).WithMessage("Título deve ter no mínimo 3 caracteres.")
                .MaximumLength(200).WithMessage("Título deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Descrição é obrigatória.")
                .MinimumLength(10).WithMessage("Descrição deve ter no mínimo 10 caracteres.")
                .MaximumLength(2000).WithMessage("Descrição deve ter no máximo 2000 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Preço não pode ser negativo.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Categoria é obrigatória.")
                .MaximumLength(100).WithMessage("Categoria deve ter no máximo 100 caracteres.");

            RuleFor(x => x.TeacherId)
                .NotEmpty().WithMessage("ID do professor é obrigatório.");
        }
    }
}
