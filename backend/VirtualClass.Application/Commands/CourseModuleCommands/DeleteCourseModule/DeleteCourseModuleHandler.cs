using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.CourseModuleCommands.DeleteCourseModule
{
    public class DeleteCourseModuleHandler : IRequestHandler<DeleteCourseModuleCommand, ServiceResult>
    {
        private readonly ICourseModuleRepository _moduleRepository;

        public DeleteCourseModuleHandler(ICourseModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<ServiceResult> Handle(DeleteCourseModuleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var module = await _moduleRepository.GetModuleByIdAsync(request.Id);
                if (module == null)
                {
                    return ServiceResult.Error(
                        "Módulo não encontrado.",
                        ErrorTypeEnum.NotFound);
                }

                await _moduleRepository.DeleteModuleAsync(module);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Error(
                    $"Erro ao excluir módulo: {ex.Message}",
                    ErrorTypeEnum.Failure);
            }
        }
    }
}