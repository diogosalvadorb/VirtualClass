using MediatR;
using VirtualClass.Application.ViewModel.CourseViewModels;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.CourseQueries.GetAllCourses
{
    public class GetAllCoursesQuery : IRequest<ServiceResult<List<CourseViewModel>>>
    {
    }
}
