namespace VirtualClass.Core.Entities
{
    public class Payment : BaseEntity
    {
        public Guid StudentId { get; private set; }
        public Student Student { get; private set; } = null!;
        public Guid CourseId { get; private set; }
        public Course Course { get; private set; } = null!;
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public PaymentMethod Method { get; private set; }
        public string? StripeSessionId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? PaidAt { get; private set; }       
    }

    public enum PaymentStatus
    {
        Pending = 1,
        Paid = 2,
        Failed = 3
    }

    public enum PaymentMethod
    {
        Pix = 1,
        CreditCard = 2
    }
}