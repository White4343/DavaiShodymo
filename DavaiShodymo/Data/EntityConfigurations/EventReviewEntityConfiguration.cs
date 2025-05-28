using DavaiShodymo.EventReviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class EventReviewEntityConfiguration : IEntityTypeConfiguration<EventReview>
{
    public void Configure(EntityTypeBuilder<EventReview> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Description);
        builder.Property(i => i.DateStamp).IsRequired();
        builder.Property(i => i.Rating).IsRequired();
        builder.HasOne(i => i.User)
            .WithMany(u => u.EventReviews)
            .HasForeignKey(u => u.UserId)
            .IsRequired();
        builder.HasOne(i => i.Event)
            .WithMany(u => u.EventReviews)
            .HasForeignKey(u => u.EventId)
            .IsRequired();
    }
}