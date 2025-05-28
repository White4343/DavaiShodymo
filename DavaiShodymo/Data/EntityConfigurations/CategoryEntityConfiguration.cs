using DavaiShodymo.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Name).IsRequired();
        builder.HasMany(i => i.Tags)
            .WithOne(u => u.Category)
            .HasForeignKey(u => u.CategoryId)
            .IsRequired();
        builder.HasMany(i => i.EventFilters)
            .WithOne(u => u.Category)
            .HasForeignKey(u => u.CategoryId)
            .IsRequired(false);
    }
}