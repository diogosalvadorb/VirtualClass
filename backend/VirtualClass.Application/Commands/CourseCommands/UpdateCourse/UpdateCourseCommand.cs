using MediatR;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.CourseCommands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<ServiceResult>
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
