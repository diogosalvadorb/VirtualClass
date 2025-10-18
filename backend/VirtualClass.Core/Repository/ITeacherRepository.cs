using VirtualClass.Core.Entities;

namespace VirtualClass.Core.Repository
{
    public interface ITeacherRepository
    {
        Task<Teacher?> GetTeacherByIdAsync(Guid id);
        Task<Teacher?> GetTeacherByUserIdAsync(Guid userId);
        Task<List<Teacher>> GetAllTeachersAsync();
        Task CreateTeacherAsync(Teacher teacher);
        Task UpdateTeacherAsync(Teacher teacher);
    }
}
