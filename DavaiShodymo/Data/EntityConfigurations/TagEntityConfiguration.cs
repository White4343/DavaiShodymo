using DavaiShodymo.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class TagEntityConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Name).IsRequired();
        builder.HasMany(i => i.EventFilters)
            .WithOne(u => u.Tag)
            .HasForeignKey(u => u.TagId)
            .IsRequired(false);
        builder.HasOne(i => i.Category)
            .WithMany(u => u.Tags)
            .HasForeignKey(u => u.CategoryId)
            .IsRequired();
    }
}