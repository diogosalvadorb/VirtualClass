namespace VirtualClass.Core.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public int RoleId { get; private set; }
        public Role Role { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    }
}
