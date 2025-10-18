namespace VirtualClass.Core.Entities
{
    public class CourseModule : BaseEntity
    {
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public int Order { get; private set; }
        public Guid CourseId { get; private set; }
        public Course Course { get; private set; } = null!;
        public ICollection<VideoLesson> Lessons { get; private set; } = new List<VideoLesson>();
    }
}
