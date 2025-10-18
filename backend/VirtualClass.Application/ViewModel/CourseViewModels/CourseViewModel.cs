namespace VirtualClass.Application.ViewModel.CourseViewModels
{
    public class CourseViewModel
    {
        public CourseViewModel(Guid id, string title, string description, decimal price,
            string category, string teacherName, bool isActive, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Category = category;
            TeacherName = teacherName;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string TeacherName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int EnrollmentsCount { get; set; }
    }
}
