namespace VirtualClass.Core.Entities
{
    public class VideoLesson : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string VideoUrl { get; private set; } = string.Empty;
        public int Order { get; private set; }
        public Guid CourseModuleId { get; private set; }
        public CourseModule CourseModule { get; private set; } = null!;
    }
}