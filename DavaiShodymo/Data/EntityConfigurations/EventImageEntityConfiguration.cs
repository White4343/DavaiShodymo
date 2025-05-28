using DavaiShodymo.EventImages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class EventImageEntityConfiguration : IEntityTypeConfiguration<EventImage>
{
    public void Configure(EntityTypeBuilder<EventImage> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.HasKey(i => i.Id);
        builder.Property(i => i.PhotoLink).IsRequired();
        builder.HasOne(i => i.EventImagePlacement)
            .WithMany(u => u.EventImages)
            .HasForeignKey(u => u.EventImagePlacementId)
            .IsRequired();
        builder.HasOne(i => i.Event)
            .WithMany(u => u.EventImages)
            .HasForeignKey(u => u.EventId)
            .IsRequired();
    }
}