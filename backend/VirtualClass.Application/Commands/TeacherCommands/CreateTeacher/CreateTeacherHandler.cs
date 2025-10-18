using MediatR;
using VirtualClass.Application.ViewModel.TeacherViewModels;
using VirtualClass.Core.Entities;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.TeacherCommands.CreateTeacher
{
    public class CreateTeacherHandler : IRequestHandler<CreateTeacherCommand, ServiceResult<TeacherViewModel>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        public CreateTeacherHandler(ITeacherRepository teacherRepository, IUserRepository userRepository)
        {
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
        }

        public async Task<ServiceResult<TeacherViewModel>> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.UserId);
                if (user == null)
                {
                    return ServiceResult<TeacherViewModel>.Error(
                        "Usuário não encontrado", ErrorTypeEnum.NotFound);
                }

                var teacher = new Teacher(request.UserId, request.Bio, request.Specialty);
                await _teacherRepository.CreateTeacherAsync(teacher);

                var viewModel = new TeacherViewModel(
                    teacher.Id,
                    teacher.Bio,
                    teacher.Specialty);

                return ServiceResult<TeacherViewModel>.Success(viewModel);
            }
            catch (Exception ex)
            {
                return ServiceResult<TeacherViewModel>.Error(
                    $"Erro ao criar professor: {ex.Message}", ErrorTypeEnum.Failure);
            }
        }
    }
}
