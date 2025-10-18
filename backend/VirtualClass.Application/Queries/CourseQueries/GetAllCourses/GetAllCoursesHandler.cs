using MediatR;
using VirtualClass.Application.ViewModel.CourseViewModels;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.CourseQueries.GetAllCourses
{
    public class GetAllCoursesHandler : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseViewModel>>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetAllCoursesHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<ServiceResult<List<CourseViewModel>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var courses = await _courseRepository.GetAllCoursesAsync();

                var viewModels = courses.Select(c => new CourseViewModel(
                    c.Id,
                    c.Title,
                    c.Description,
                    c.Price,
                    c.Category,
                    c.Teacher.User.FullName,
                    c.IsActive,
                    c.CreatedAt)).ToList();

                return ServiceResult<List<CourseViewModel>>.Success(viewModels);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<CourseViewModel>>.Error(
                    $"Erro ao buscar cursos: {ex.Message}",
                    ErrorTypeEnum.Failure);
            }
        }
    }
}
