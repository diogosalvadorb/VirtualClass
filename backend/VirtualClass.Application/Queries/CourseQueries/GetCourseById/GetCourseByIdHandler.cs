using MediatR;
using VirtualClass.Application.ViewModel.CourseModuleViewModels;
using VirtualClass.Application.ViewModel.CourseViewModels;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.CourseQueries.GetCourseById
{
    public class GetCourseByIdHandler : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDetailViewModel>>
    {
        private readonly ICourseRepository _courseRepository;
        public GetCourseByIdHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<ServiceResult<CourseDetailViewModel>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetCourseByIdAsync(request.Id);
                if (course == null)
                {
                    return ServiceResult<CourseDetailViewModel>.Error(
                        "Curso não encontrado.",
                        ErrorTypeEnum.NotFound);
                }

                var modules = course.Modules.Select(m => new CourseModuleViewModel(
                    m.Id,
                    m.Title,
                    m.Description,
                    m.Order)).ToList();

                var viewModel = new CourseDetailViewModel(
                    course.Id,
                    course.Title,
                    course.Description,
                    course.Price,
                    course.Category,
                    course.Teacher.User.FullName,
                    course.IsActive,
                    course.CreatedAt,
                    course.Enrollments.Count,
                    modules.Count,
                    modules);

                return ServiceResult<CourseDetailViewModel>.Success(viewModel);
            }
            catch (Exception ex)
            {
                return ServiceResult<CourseDetailViewModel>.Error(
                    $"Erro ao buscar curso: {ex.Message}",
                    ErrorTypeEnum.Failure);
            }
        }
    }
}