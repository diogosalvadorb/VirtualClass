using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.CourseModuleCommands.UpdateCourseModule
{
    public class UpdateCourseModuleHandler : IRequestHandler<UpdateCourseModuleCommand, ServiceResult>
    {
        private readonly ICourseModuleRepository _moduleRepository;

        public UpdateCourseModuleHandler(ICourseModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<ServiceResult> Handle(UpdateCourseModuleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var module = await _moduleRepository.GetModuleByIdAsync(request.ModuleId);
                if (module == null)
                {
                    return ServiceResult.Error(
                        "Módulo não encontrado.",
                        ErrorTypeEnum.NotFound);
                }

                module.Update(request.Title, request.Description, request.Order);
                await _moduleRepository.UpdateModuleAsync(module);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Error(
                    $"Erro ao atualizar módulo: {ex.Message}",
                    ErrorTypeEnum.Failure);
            }
        }
    }
}