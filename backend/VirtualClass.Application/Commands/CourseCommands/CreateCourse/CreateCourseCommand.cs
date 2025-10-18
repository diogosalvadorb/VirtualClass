using MediatR;
using VirtualClass.Application.ViewModel.CourseViewModels;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.CourseCommands.CreateCourse
{
    public class CreateCourseCommand : IRequest<ServiceResult<CourseViewModel>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public Guid TeacherId { get; set; }
    }
}
