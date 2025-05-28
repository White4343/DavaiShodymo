using DavaiShodymo.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.HasKey(i => i.Id);
        builder.Property(i => i.DateStart).IsRequired();
        builder.Property(i => i.DateEnd).IsRequired();
        builder.Property(i => i.DateStamp).IsRequired();
        builder.Property(i => i.Name).IsRequired();
        builder.Property(i => i.Description);
        builder.Property(i => i.Location);
        builder.Property(i => i.IsActive).IsRequired();
        builder.HasOne(i => i.User)
            .WithMany(u => u.Events)
            .HasForeignKey(u => u.UserId)
            .IsRequired();
        builder.HasMany(i => i.EventImages)
            .WithOne(u => u.Event)
            .HasForeignKey(u => u.EventId)
            .IsRequired();
        builder.HasMany(i => i.EventReviews)
            .WithOne(u => u.Event)
            .HasForeignKey(u => u.EventId)
            .IsRequired();
        builder.HasMany(i => i.EventFilters)
            .WithOne(u => u.Event)
            .HasForeignKey(u => u.EventId)
            .IsRequired();
        builder.HasMany(i => i.TicketCategories)
            .WithOne(u => u.Event)
            .HasForeignKey(u => u.EventId)
            .IsRequired();
    }
}