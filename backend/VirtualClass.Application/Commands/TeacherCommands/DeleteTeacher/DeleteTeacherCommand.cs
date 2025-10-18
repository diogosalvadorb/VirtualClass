using MediatR;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.TeacherCommands.DeleteTeacher
{
    public class DeleteTeacherCommand : IRequest<ServiceResult>
    {
        public DeleteTeacherCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
