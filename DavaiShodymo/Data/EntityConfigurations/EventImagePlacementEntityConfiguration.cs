using DavaiShodymo.EventImagePlacements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class EventImagePlacementEntityConfiguration : IEntityTypeConfiguration<EventImagePlacement>
{
    public void Configure(EntityTypeBuilder<EventImagePlacement> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Placement).IsRequired();
        builder.HasMany(i => i.EventImages)
            .WithOne(u => u.EventImagePlacement)
            .HasForeignKey(u => u.EventImagePlacementId)
            .IsRequired();
    }
}