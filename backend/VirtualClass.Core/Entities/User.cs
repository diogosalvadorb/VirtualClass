namespace VirtualClass.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, string passwordHash)
        {
            FullName = fullName;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.Now;
            RoleId = 2;
            IsEmailConfirmed = false;
            EmailConfirmationToken = Guid.NewGuid().ToString();
        }

        public string FullName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public int RoleId { get; private set; }
        public Role Role { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public bool IsEmailConfirmed { get; private set; }
        public string? EmailConfirmationToken { get; private set; }

        public void ConfirmEmail()
        {
            IsEmailConfirmed = true;
            EmailConfirmationToken = null;
        }
    }
}
