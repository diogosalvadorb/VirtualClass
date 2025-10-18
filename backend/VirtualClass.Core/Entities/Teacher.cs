namespace VirtualClass.Core.Entities
{
    public class Teacher : BaseEntity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; } 
        public string Bio { get; private set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
