namespace VirtualClass.Core.Entities
{
    public class Course : BaseEntity
    {
        public Course(string title, string description, decimal price, string category, Guid teacherId)
        {
            Title = title;
            Description = description;
            Price = price;
            Category = category;
            TeacherId = teacherId;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string Category { get; private set; } = string.Empty;
        public Guid TeacherId { get; private set; }
        public Teacher Teacher { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }
        public ICollection<CourseModule> Modules { get; private set; } = new List<CourseModule>();
        public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();
        public ICollection<Payment> Payments { get; private set; } = new List<Payment>();

        public void Update(string title, string description, decimal price, string category)
        {
            Title = title;
            Description = description;
            Price = price;
            Category = category;
        }
    }
}
