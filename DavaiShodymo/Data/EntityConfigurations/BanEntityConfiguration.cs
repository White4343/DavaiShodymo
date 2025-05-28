using DavaiShodymo.Bans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class BanEntityConfiguration : IEntityTypeConfiguration<Ban>
{
    public void Configure(EntityTypeBuilder<Ban> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Reason).IsRequired();

        builder.Property(i => i.DateEnd).IsRequired();

        builder.Property(i => i.DateStamp).IsRequired();

        builder.Property(i => i.IsActive).IsRequired();

        builder.HasOne(i => i.User)
            .WithMany(u => u.Bans)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        builder.HasOne(i => i.AdminUser)
            .WithMany(u => u.BannedBy)
            .HasForeignKey(u => u.AdminUserId)
            .IsRequired();
    }
}