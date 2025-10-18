using MediatR;
using VirtualClass.Application.ViewModel.CourseModuleViewModels;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.CourseModuleQueries.GetCourseModuleById
{
    public class GetCourseModuleByIdQuery : IRequest<ServiceResult<CourseModuleDetailViewModel>>
    {
        public GetCourseModuleByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
