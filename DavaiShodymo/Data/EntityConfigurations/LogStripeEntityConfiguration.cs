using DavaiShodymo.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class LogStripeEntityConfiguration : IEntityTypeConfiguration<LogStripe>
{
    public void Configure(EntityTypeBuilder<LogStripe> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Price);
        builder.HasOne(i => i.TicketCategory)
            .WithMany(u => u.LogStripes)
            .HasForeignKey(u => u.TicketCategoryId)
            .IsRequired(false);
        builder.HasOne(i => i.Subscription)
            .WithMany(u => u.LogStripes)
            .HasForeignKey(u => u.SubscriptionId)
            .IsRequired(false);
        builder.HasOne(i => i.Log)
            .WithMany(u => u.LogStripes)
            .HasForeignKey(u => u.LogId)
            .IsRequired();
    }
}