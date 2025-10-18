using MediatR;
using VirtualClass.Application.ViewModel.CourseModuleViewModels;
using VirtualClass.Application.ViewModel.VideoLessonViewModels;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Queries.CourseModuleQueries.GetCourseModuleById
{
    public class GetCourseModuleByIdHandler : IRequestHandler<GetCourseModuleByIdQuery, ServiceResult<CourseModuleDetailViewModel>>
    {
        private readonly ICourseModuleRepository _moduleRepository;
        public GetCourseModuleByIdHandler(ICourseModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<ServiceResult<CourseModuleDetailViewModel>> Handle(GetCourseModuleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var module = await _moduleRepository.GetModuleWithLessonsAsync(request.Id);
                if (module == null)
                {
                    return ServiceResult<CourseModuleDetailViewModel>.Error(
                        "Módulo não encontrado.",
                        ErrorTypeEnum.NotFound);
                }

                var lessons = module.Lessons.Select(l => new VideoLessonViewModel(
                    l.Id,
                    l.Title,
                    l.Description,
                    l.VideoUrl,
                    l.Order)).ToList();

                var viewModel = new CourseModuleDetailViewModel(
                    module.Id,
                    module.Title,
                    module.Description,
                    module.Order,
                    module.CourseId,
                    module.Course.Title,
                    lessons);

                return ServiceResult<CourseModuleDetailViewModel>.Success(viewModel);
            }
            catch (Exception ex)
            {
                return ServiceResult<CourseModuleDetailViewModel>.Error(
                    $"Erro ao buscar módulo: {ex.Message}",
                    ErrorTypeEnum.Failure);
            }
        }
    }
}