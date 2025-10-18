using MediatR;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.TeacherCommands.UpdateTeacher
{
    public class UpdateTeacherCommand : IRequest<ServiceResult>
    {
        public Guid TeacherId { get; private set; }
        public string Bio { get; private set; } = string.Empty;
        public string Specialty { get; private set; } = string.Empty;
    }
}
