using MediatR;
using VirtualClass.Application.ViewModel.TeacherViewModels;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.TeacherCommands.CreateTeacher
{
    public class CreateTeacherCommand : IRequest<ServiceResult<TeacherViewModel>>
    {
        public Guid UserId { get; private set; }
        public string Bio { get; private set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
    }
}
