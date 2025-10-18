using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.TeacherCommands.DeleteTeacher
{
    public class DeleteTeacherHandler : IRequestHandler<DeleteTeacherCommand, ServiceResult>
    {
        private readonly ITeacherRepository _teacherRepository;

        public DeleteTeacherHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<ServiceResult> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var teacher = await _teacherRepository.GetTeacherByIdAsync(request.Id);
                if (teacher == null)
                {
                    return ServiceResult.Error(
                        "Professor não encontrado.",
                        ErrorTypeEnum.NotFound);
                }

                if (teacher.Courses.Any())
                {
                    return ServiceResult.Error(
                        "Não é possível excluir um professor que possui cursos cadastrados.",
                        ErrorTypeEnum.Validation);
                }

                await _teacherRepository.DeleteTeacherAsync(teacher);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Error(
                    $"Erro ao excluir professor: {ex.Message}",
                    ErrorTypeEnum.Failure);
            }
        }
    }
}