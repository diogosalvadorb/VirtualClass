using MediatR;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.CourseModuleCommands.UpdateCourseModule
{
    public class UpdateCourseModuleCommand : IRequest<ServiceResult>
    {
        public Guid ModuleId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
