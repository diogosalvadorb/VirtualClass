using Microsoft.EntityFrameworkCore;
using VirtualClass.Core.Entities;
using VirtualClass.Core.Repository;

namespace VirtualClass.Infrastructure.Persistence.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly VirtualClassDbContext _context;
        public TeacherRepository(VirtualClassDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher?> GetTeacherByIdAsync(Guid id)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Courses)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Teacher?> GetTeacherByUserIdAsync(Guid userId)
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Courses)
                .SingleOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await _context.Teachers
                .Include(t => t.User)
                .Include(t => t.Courses)
                .ToListAsync();
        }

        public async Task CreateTeacherAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);  
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }
    }
}
