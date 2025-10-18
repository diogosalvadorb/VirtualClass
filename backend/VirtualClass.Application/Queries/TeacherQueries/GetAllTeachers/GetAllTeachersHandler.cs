using MediatR;
using VirtualClass.Application.ViewModel.TeacherViewModels;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.TeacherQueries.GetAllTeachers
{
    public class GetAllTeachersHandler : IRequestHandler<GetAllTeachersQuery, ServiceResult<List<TeacherViewModel>>>
    {
        private readonly ITeacherRepository _teacherRepository;
        public GetAllTeachersHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<ServiceResult<List<TeacherViewModel>>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var teachers = await _teacherRepository.GetAllTeachersAsync();

                var teacherViewModels = teachers.Select(t => new TeacherViewModel(t.Id, t.Bio, t.Specialty)).ToList();

                return ServiceResult<List<TeacherViewModel>>.Success(teacherViewModels);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<TeacherViewModel>>.Error(
                    $"Erro ao buscar professores: {ex.Message}", ErrorTypeEnum.Failure);
            }
        }
    }
}
