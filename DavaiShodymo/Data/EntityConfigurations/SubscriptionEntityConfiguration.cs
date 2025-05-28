using DavaiShodymo.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class SubscriptionEntityConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(i => i.Id);
        builder.Property(i => i.DateRange).IsRequired();
        builder.Property(i => i.Name).IsRequired();
        builder.Property(i => i.Price);
        builder.Property(i => i.DateStamp).IsRequired();
        builder.HasMany(i => i.UserSubscriptions)
            .WithOne(u => u.Subscription)
            .HasForeignKey(u => u.SubscriptionId)
            .IsRequired();
        builder.HasMany(i => i.LogStripes)
            .WithOne(u => u.Subscription)
            .HasForeignKey(u => u.SubscriptionId)
            .IsRequired();
    }
}