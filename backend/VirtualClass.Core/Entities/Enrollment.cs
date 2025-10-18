namespace VirtualClass.Core.Entities
{
    public class Enrollment : BaseEntity
    {
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; } = null!;
        public Guid CourseId { get; private set; }
        public Course Course { get; private set; } = null!;
        public DateTime EnrollmentDate { get; private set; }
        public EnrollmentStatus Status { get; private set; }
        public Guid? PaymentId { get; private set; }
        public Payment? Payment { get; private set; }
    }

    public enum EnrollmentStatus
    {
        Active = 1,
        Completed = 2,
        Cancelled = 3
    }
}