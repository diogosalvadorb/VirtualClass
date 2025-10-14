using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualClass.Core.Entities;

namespace VirtualClass.Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(250);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(350);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();
            
            builder.HasIndex(u => u.RoleId);
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
