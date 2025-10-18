using MediatR;
using VirtualClass.Application.ViewModel.CourseModuleViewModels;
using VirtualClass.Core.Entities;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.CourseModuleCommands.CreateCourseModule
{
    public class CreateCourseModuleHandler : IRequestHandler<CreateCourseModuleCommand, ServiceResult<CourseModuleViewModel>>
    {
        private readonly ICourseModuleRepository _moduleRepository;
        private readonly ICourseRepository _courseRepository;

        public CreateCourseModuleHandler(ICourseModuleRepository moduleRepository, ICourseRepository courseRepository)
        {
            _moduleRepository = moduleRepository;
            _courseRepository = courseRepository;
        }

        public async Task<ServiceResult<CourseModuleViewModel>> Handle(CreateCourseModuleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetCourseByIdAsync(request.CourseId);
                if (course == null)
                {
                    return ServiceResult<CourseModuleViewModel>.Error(
                        "Curso não encontrado.",
                        ErrorTypeEnum.NotFound);
                }

                var module = new CourseModule(
                    request.Title,
                    request.Description,
                    request.Order,
                    request.CourseId);

                await _moduleRepository.CreateModuleAsync(module);

                var viewModel = new CourseModuleViewModel(
                    module.Id,
                    module.Title,
                    module.Description,
                    module.Order);

                return ServiceResult<CourseModuleViewModel>.Success(viewModel);
            }
            catch (Exception ex)
            {
                return ServiceResult<CourseModuleViewModel>.Error(
                    $"Erro ao criar módulo: {ex.Message}",
                    ErrorTypeEnum.Failure);
            }
        }
    }
}