using DavaiShodymo.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.UserName).IsRequired();

        builder.Property(i => i.NormalizedUserName).IsRequired();

        builder.Property(i => i.Email).IsRequired();

        builder.Property(i => i.NormalizedEmail).IsRequired();

        builder.Property(i => i.PasswordHash).IsRequired();

        builder.Property(i => i.FirstName).IsRequired(false);

        builder.Property(i => i.LastName).IsRequired(false);

        builder.Property(i => i.PhotoLink).IsRequired(false);

        builder.Property(i => i.BirthDate).IsRequired(false);

        builder.Property(i => i.CreatedDate).IsRequired();

        builder.Property(i => i.UpdatedDate).IsRequired();

        builder.HasOne(i => i.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(i => i.RoleId)
            .IsRequired();

        builder.HasMany(i => i.Bans)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        builder.HasMany(i => i.BannedBy)
            .WithOne(u => u.AdminUser)
            .HasForeignKey(u => u.AdminUserId)
            .IsRequired();

        builder.HasMany(i => i.UserSubscriptions)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        builder.HasMany(i => i.Logs)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        builder.HasMany(i => i.Events)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        builder.HasMany(i => i.EventReviews)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired();

        builder.HasMany(i => i.EventEnrolments)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .IsRequired();
    }
}