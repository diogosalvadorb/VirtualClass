using FluentValidation;
using VirtualClass.Application.Commands.TeacherCommands.UpdateTeacher;

namespace VirtualClass.Application.Validators.TeacherValidators
{
    internal class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherCommandValidator()
        {
            RuleFor(x => x.TeacherId)
                .NotEmpty().WithMessage("ID do professor é obrigatório.");

            RuleFor(x => x.Bio)
                .NotEmpty().WithMessage("Biografia é obrigatória.")
                .MinimumLength(10).WithMessage("Biografia deve ter no mínimo 10 caracteres.")
                .MaximumLength(1000).WithMessage("Biografia deve ter no máximo 1000 caracteres.");

            RuleFor(x => x.Specialty)
                .NotEmpty().WithMessage("Especialidade é obrigatória.")
                .MinimumLength(3).WithMessage("Especialidade deve ter no mínimo 3 caracteres.")
                .MaximumLength(200).WithMessage("Especialidade deve ter no máximo 200 caracteres.");
        }
    }
}