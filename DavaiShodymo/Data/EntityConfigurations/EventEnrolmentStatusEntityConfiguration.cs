using DavaiShodymo.EventEnrolmentStatuses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class EventEnrolmentStatusEntityConfiguration : IEntityTypeConfiguration<EventEnrolmentStatus>
{
    public void Configure(EntityTypeBuilder<EventEnrolmentStatus> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Status).IsRequired();
        builder.HasMany(i => i.EventEnrolments)
            .WithOne(u => u.EventEnrolmentStatus)
            .HasForeignKey(u => u.EventEnrolmentStatusId)
            .IsRequired(false);
    }
}