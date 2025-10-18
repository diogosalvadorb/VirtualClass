using MediatR;
using VirtualClass.Application.ViewModel.TeacherViewModels;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.TeacherQueries.GetTeacherById
{
    public class GetTeacherByIdHandler : IRequestHandler<GetTeacherByIdQuery, ServiceResult<TeacherViewModel>>
    {
        private readonly ITeacherRepository _teacherRepository;
        public GetTeacherByIdHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        public async Task<ServiceResult<TeacherViewModel>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var teacher = await _teacherRepository.GetTeacherByIdAsync(request.Id);
                if (teacher == null)
                {
                    return ServiceResult<TeacherViewModel>.Error("Professor não encontrado.", ErrorTypeEnum.NotFound);
                }

                var teacherViewModel = new TeacherViewModel(teacher.Id, teacher.Bio, teacher.Specialty);

                return ServiceResult<TeacherViewModel>.Success(teacherViewModel);
            }
            catch (Exception ex)
            {
                return ServiceResult<TeacherViewModel>.Error(
                    $"Erro ao buscar professor: {ex.Message}", ErrorTypeEnum.Failure);
            }
        }
    }
}
