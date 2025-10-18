using FluentValidation;
using VirtualClass.Application.Commands.CourseCommands.UpdateCourse;

namespace VirtualClass.Application.Validators.CourseValidators
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("ID do curso é obrigatório.");

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
        }
    }
}
