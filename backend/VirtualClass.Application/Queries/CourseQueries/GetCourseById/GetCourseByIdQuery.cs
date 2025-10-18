using MediatR;
using VirtualClass.Application.ViewModel.CourseViewModels;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.CourseQueries.GetCourseById
{
    public class GetCourseByIdQuery : IRequest<ServiceResult<CourseDetailViewModel>>
    {
        public GetCourseByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}