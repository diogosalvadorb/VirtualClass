using MediatR;
using VirtualClass.Core.Repository;
using VirtualClass.Core.Results;

namespace VirtualClass.Application.Commands.CourseCommands.UpdateCourse
{
    public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var course = await _courseRepository.GetCourseByIdAsync(request.CourseId);
                if (course == null)
                {
                    return ServiceResult.Error(
                        "Curso não encontrado.",
                        ErrorTypeEnum.NotFound);
                }

                course.Update(request.Title, request.Description, request.Price, request.Category);
                await _courseRepository.UpdateCourseAsync(course);

                return ServiceResult.Success();
            }
            catch (Exception ex)
            {
                return ServiceResult.Error(
                    $"Erro ao atualizar curso: {ex.Message}",
                    ErrorTypeEnum.Failure);
            }
        }
    }
}
