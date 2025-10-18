using MediatR;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.CourseModuleCommands.DeleteCourseModule
{
    public class DeleteCourseModuleCommand : IRequest<ServiceResult>
    {
        public DeleteCourseModuleCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
