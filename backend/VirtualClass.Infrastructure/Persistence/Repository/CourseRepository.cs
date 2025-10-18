using Microsoft.EntityFrameworkCore;
using VirtualClass.Core.Entities;
using VirtualClass.Core.Repository;

namespace VirtualClass.Infrastructure.Persistence.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly VirtualClassDbContext _context;
        public CourseRepository(VirtualClassDbContext context)
        {
            _context = context;
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            return await _context.Courses
                .Include(c => c.Teacher)
                    .ThenInclude(t => t.User)
                .Include(c => c.Modules.OrderBy(m => m.Order))
                    .ThenInclude(m => m.Lessons.OrderBy(l => l.Order))
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Course>> GetCoursesByTeacherIdAsync(Guid teacherId)
        {
            return await _context.Courses
                .Include(c => c.Teacher)
                    .ThenInclude(t => t.User)
                .Include(c => c.Enrollments)
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses
                .Include(c => c.Teacher)
                .ThenInclude(t => t.User)
                .ToListAsync();
        }

        public async Task CreateCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();  
        }

        public async Task UpdateCourseAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
    }
}
