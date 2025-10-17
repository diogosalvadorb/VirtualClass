using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualClass.Core.Entities;

namespace VirtualClass.Infrastructure.Persistence.Configuration
{
    public class VideoLessonConfiguration : IEntityTypeConfiguration<VideoLesson>
    {
        public void Configure(EntityTypeBuilder<VideoLesson> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(l => l.VideoUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(l => l.Order)
                .IsRequired();

            builder.HasIndex(l => new { l.ModuleId, l.Order });

            builder.HasOne(l => l.Module)
                .WithMany(m => m.Lessons)
                .HasForeignKey(l => l.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}