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
        public string? PasswordResetToken { get; private set; }
        public DateTime? PasswordResetTokenExpiry { get; private set; }

        public void ConfirmEmail()
        {
            IsEmailConfirmed = true;
            EmailConfirmationToken = null;
        }

        public void GeneratePasswordResetToken()
        {
            PasswordResetToken = Guid.NewGuid().ToString();
            PasswordResetTokenExpiry = DateTime.Now.AddHours(24);
        }

        public void ResetPassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            PasswordResetToken = null;
            PasswordResetTokenExpiry = null;
        }

        public bool IsPasswordResetTokenValid()
        {
            return PasswordResetToken != null &&
                   PasswordResetTokenExpiry.HasValue &&
                   PasswordResetTokenExpiry.Value > DateTime.Now;
        }
    }
}
