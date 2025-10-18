using MediatR;
using VirtualClass.Application.ViewModel.TeacherViewModels;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.TeacherQueries.GetAllTeachers
{
    public class GetAllTeachersQuery : IRequest<ServiceResult<List<TeacherViewModel>>>
    {
    }
}
