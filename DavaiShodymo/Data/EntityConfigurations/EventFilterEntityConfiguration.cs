using DavaiShodymo.EventFilters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class EventFilterEntityConfiguration : IEntityTypeConfiguration<EventFilter>
{
    public void Configure(EntityTypeBuilder<EventFilter> builder)
    {
        builder.HasKey(i => i.Id);
        builder.HasOne(i => i.Tag)
            .WithMany(u => u.EventFilters)
            .HasForeignKey(u => u.TagId)
            .IsRequired(false);
        builder.HasOne(i => i.Category)
            .WithMany(u => u.EventFilters)
            .HasForeignKey(u => u.CategoryId)
            .IsRequired(false);
        builder.HasOne(i => i.Event)
            .WithMany(u => u.EventFilters)
            .HasForeignKey(u => u.EventId)
            .IsRequired();
    }
}