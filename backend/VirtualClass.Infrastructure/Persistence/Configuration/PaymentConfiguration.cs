using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualClass.Core.Entities;

namespace VirtualClass.Infrastructure.Persistence.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.Method)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(p => p.StripeSessionId)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.PaidAt)
                .IsRequired(false);

            builder.HasIndex(p => p.StudentId);
            builder.HasIndex(p => p.CourseId);
            builder.HasIndex(p => p.Status);
            builder.HasIndex(p => p.StripeSessionId);

            builder.HasOne(p => p.Student)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Course)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}