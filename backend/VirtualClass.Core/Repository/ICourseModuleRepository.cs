using VirtualClass.Core.Entities;

namespace VirtualClass.Core.Repository
{
    public interface ICourseModuleRepository
    {
        Task<CourseModule?> GetModuleByIdAsync(Guid id);
        Task<CourseModule?> GetModuleWithLessonsAsync(Guid id);
        Task<List<CourseModule>> GetModulesByCourseIdAsync(Guid courseId);
        Task CreateModuleAsync(CourseModule module);
        Task UpdateModuleAsync(CourseModule module);
        Task DeleteModuleAsync(CourseModule module);
    }
}
