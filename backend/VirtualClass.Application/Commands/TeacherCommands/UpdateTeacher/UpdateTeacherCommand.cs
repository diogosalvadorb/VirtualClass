using MediatR;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.TeacherCommands.UpdateTeacher
{
    public class UpdateTeacherCommand : IRequest<ServiceResult>
    {
        public Guid TeacherId { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
    }
}
