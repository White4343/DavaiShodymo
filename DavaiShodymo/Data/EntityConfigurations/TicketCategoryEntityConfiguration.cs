using DavaiShodymo.TicketCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class TicketCategoryEntityConfiguration : IEntityTypeConfiguration<TicketCategory>
{
    public void Configure(EntityTypeBuilder<TicketCategory> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Price).IsRequired();
        builder.Property(i => i.Amount).IsRequired();
        builder.Property(i => i.Name).IsRequired();
        builder.Property(i => i.Description);
        builder.Property(i => i.IsActive).IsRequired();
        builder.Property(i => i.ExternalPricingLink);
        builder.Property(i => i.DateStamp).IsRequired();
        builder.HasOne(i => i.Event)
            .WithMany(u => u.TicketCategories)
            .HasForeignKey(u => u.EventId)
            .IsRequired();
        builder.HasMany(i => i.LogStripes)
            .WithOne(u => u.TicketCategory)
            .HasForeignKey(u => u.TicketCategoryId)
            .IsRequired(false);
        builder.HasMany(i => i.Tickets)
            .WithOne(u => u.TicketCategory)
            .HasForeignKey(u => u.TicketCategoryId)
            .IsRequired();
    }
}