using DavaiShodymo.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class LogTypeEntityConfiguration : IEntityTypeConfiguration<LogType>
{
    public void Configure(EntityTypeBuilder<LogType> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Name).IsRequired();
        builder.HasMany(i => i.Logs)
            .WithOne(u => u.LogType)
            .HasForeignKey(u => u.LogTypeId)
            .IsRequired();
    }
}