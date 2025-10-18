using MediatR;
using VirtualClass.Application.ViewModel.CourseViewModels;
using VirtualClass.Core.Entities;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.CourseCommands.CreateCourse
{
    public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, ServiceResult<CourseViewModel>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;

        public CreateCourseHandler(ICourseRepository courseRepository, ITeacherRepository teacherRepository)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<ServiceResult<CourseViewModel>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var teacher = await _teacherRepository.GetTeacherByIdAsync(request.TeacherId);
                if (teacher == null)
                {
                    return ServiceResult<CourseViewModel>.Error(
                        "Professor não encontrado.",
                        ErrorTypeEnum.NotFound);
                }

                var course = new Course(
                    request.Title,
                    request.Description,
                    request.Price,
                    request.Category,
                    request.TeacherId);

                await _courseRepository.CreateCourseAsync(course);

                var viewModel = new CourseViewModel(
                    course.Id,
                    course.Title,
                    course.Description,
                    course.Price,
                    course.Category,
                    teacher.User.FullName,
                    course.IsActive,
                    course.CreatedAt);

                return ServiceResult<CourseViewModel>.Success(viewModel);
            }
            catch (Exception ex)
            {
                return ServiceResult<CourseViewModel>.Error(
                    $"Erro ao criar curso: {ex.Message}",
                    ErrorTypeEnum.Failure);
            }
        }
    }
}
