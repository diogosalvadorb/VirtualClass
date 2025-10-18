using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VirtualClass.Core.Entities;

namespace VirtualClass.Infrastructure.Persistence
{
    public class VirtualClassDbContext : DbContext
    {
        public VirtualClassDbContext(DbContextOptions<VirtualClassDbContext> options) : base(options) { }           

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseModule> Modules { get; set; }
        public DbSet<VideoLesson> VideoLessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
