using DavaiShodymo.UserSubscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class UserSubscriptionEntityConfiguration : IEntityTypeConfiguration<UserSubscription>
{
    public void Configure(EntityTypeBuilder<UserSubscription> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(i => i.Id);
        builder.Property(i => i.DateStart).IsRequired();
        builder.Property(i => i.DateEnd).IsRequired();
        builder.Property(i => i.DateStamp).IsRequired();
        builder.Property(i => i.Price);
        builder.Property(i => i.IsActive).IsRequired();
        builder.HasOne(i => i.User)
            .WithMany(u => u.UserSubscriptions)
            .HasForeignKey(u => u.UserId)
            .IsRequired();
        builder.HasOne(i => i.Subscription)
            .WithMany(u => u.UserSubscriptions)
            .HasForeignKey(u => u.SubscriptionId)
            .IsRequired();
    }
}