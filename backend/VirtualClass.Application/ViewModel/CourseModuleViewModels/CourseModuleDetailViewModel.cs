using VirtualClass.Application.ViewModel.VideoLessonViewModels;

namespace VirtualClass.Application.ViewModel.CourseModuleViewModels
{
    public class CourseModuleDetailViewModel
    {
        public CourseModuleDetailViewModel(Guid id, string title, string description, int order,
            Guid courseId, string courseTitle, List<VideoLessonViewModel> lessons)
        {
            Id = id;
            Title = title;
            Description = description;
            Order = order;
            CourseId = courseId;
            CourseTitle = courseTitle;
            Lessons = lessons;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public Guid CourseId { get; set; }
        public string CourseTitle { get; set; }
        public List<VideoLessonViewModel> Lessons { get; set; }
    }
}
