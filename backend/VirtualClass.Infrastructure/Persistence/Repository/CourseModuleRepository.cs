using Microsoft.EntityFrameworkCore;
using VirtualClass.Core.Entities;
using VirtualClass.Core.Repository;

namespace VirtualClass.Infrastructure.Persistence.Repository
{
    public class CourseModuleRepository : ICourseModuleRepository
    {
        private readonly VirtualClassDbContext _context;

        public CourseModuleRepository(VirtualClassDbContext context)
        {
            _context = context;
        }

        public async Task<CourseModule?> GetModuleByIdAsync(Guid id)
        {
            return await _context.Modules
                .Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<CourseModule?> GetModuleWithLessonsAsync(Guid id)
        {
            return await _context.Modules
                .Include(m => m.Course)
                .Include(m => m.Lessons.OrderBy(l => l.Order))
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<CourseModule>> GetModulesByCourseIdAsync(Guid courseId)
        {
            return await _context.Modules
                .Include(m => m.Lessons)
                .Where(m => m.CourseId == courseId)
                .OrderBy(m => m.Order)
                .ToListAsync();
        }

        public async Task CreateModuleAsync(CourseModule module)
        {
            await _context.Modules.AddAsync(module);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateModuleAsync(CourseModule module)
        {
            _context.Modules.Update(module);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteModuleAsync(CourseModule module)
        {
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
        }
    }
}