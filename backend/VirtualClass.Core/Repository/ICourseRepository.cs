using VirtualClass.Core.Entities;

namespace VirtualClass.Core.Repository
{
    public interface ICourseRepository
    {
        Task<Course?> GetCourseByIdAsync(Guid id);
        Task<List<Course>> GetAllCoursesAsync();
        Task<List<Course>> GetCoursesByTeacherIdAsync(Guid teacherId);
        Task CreateCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
    }
}
