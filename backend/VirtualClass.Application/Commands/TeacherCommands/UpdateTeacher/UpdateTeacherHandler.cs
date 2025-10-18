using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.TeacherCommands.UpdateTeacher
{
    public class UpdateTeacherHandler : IRequestHandler<UpdateTeacherCommand, ServiceResult>
    {
        private readonly ITeacherRepository _teacherRepository;
        public UpdateTeacherHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<ServiceResult> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var teacher = await _teacherRepository.GetTeacherByIdAsync(request.TeacherId);
                if (teacher == null)
                {
                    return ServiceResult.Error("Professor não encontrado.", ErrorTypeEnum.NotFound);
                }

                teacher.UpdateBio(request.Bio, request.Specialty);
                await _teacherRepository.UpdateTeacherAsync(teacher);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Error(
                    $"Erro ao atualizar o professor: {ex.Message}", ErrorTypeEnum.Failure);
            }
        }
    }
}
