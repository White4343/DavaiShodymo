using DavaiShodymo.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class LogEntityConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(i => i.Id);
        builder.Property(i => i.DateStamp).IsRequired();
        builder.HasOne(i => i.LogType)
            .WithMany(u => u.Logs)
            .HasForeignKey(u => u.LogTypeId)
            .IsRequired();
        builder.HasOne(i => i.User)
            .WithMany(u => u.Logs)
            .HasForeignKey(u => u.UserId)
            .IsRequired(false);
        builder.HasMany(i => i.LogStripes)
            .WithOne(u => u.Log)
            .HasForeignKey(u => u.LogId)
            .IsRequired(false);
    }
}