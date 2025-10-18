namespace VirtualClass.Core.Entities
{
    public class Teacher : BaseEntity
    {
        public Teacher(Guid userId, string bio, string specialty)
        {
            UserId = userId;
            Bio = bio;
            Specialty = specialty;
        }

        public Guid UserId { get; private set; }
        public User User { get; private set; } 
        public string Bio { get; private set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public void UpdateBio(string bio, string specialty)
        {
            Bio = bio;
            Specialty = specialty;
        }
    }
}
