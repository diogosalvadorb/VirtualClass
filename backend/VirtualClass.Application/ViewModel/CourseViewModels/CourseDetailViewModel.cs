using VirtualClass.Application.ViewModel.CourseModuleViewModels;

namespace VirtualClass.Application.ViewModel.CourseViewModels
{
    public class CourseDetailViewModel
    {
        public CourseDetailViewModel(Guid id, string title, string description, decimal price,
            string category, string teacherName, bool isActive, DateTime createdAt,
            int enrollmentsCount, int modulesCount, List<CourseModuleViewModel> modules)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Category = category;
            TeacherName = teacherName;
            IsActive = isActive;
            CreatedAt = createdAt;
            Modules = modules;
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
        public int ModulesCount { get; set; }
        public List<CourseModuleViewModel> Modules { get; set; }
    }
}

