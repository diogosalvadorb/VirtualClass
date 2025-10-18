using MediatR;
using VirtualClass.Application.ViewModel.CourseModuleViewModels;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.CourseModuleCommands.CreateCourseModule
{
    public class CreateCourseModuleCommand : IRequest<ServiceResult<CourseModuleViewModel>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Order { get; set; }
        public Guid CourseId { get; set; }
    }
}
