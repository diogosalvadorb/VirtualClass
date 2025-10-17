namespace VirtualClass.Core.Entities
{
    public class Student : BaseEntity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;
        public DateTime BirthDate { get; private set; }
        public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();
        public ICollection<Payment> Payments { get; private set; } = new List<Payment>();
    }
}