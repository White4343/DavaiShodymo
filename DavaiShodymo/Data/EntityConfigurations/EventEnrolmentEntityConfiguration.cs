using DavaiShodymo.EventEnrolments;
using DavaiShodymo.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DavaiShodymo.Data.EntityConfigurations;

public class EventEnrolmentEntityConfiguration : IEntityTypeConfiguration<EventEnrolment>
{
    public void Configure(EntityTypeBuilder<EventEnrolment> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.HasKey(i => i.Id);
        builder.Property(i => i.DateStamp).IsRequired();
        builder.Property(i => i.IsFavorite).IsRequired();
        builder.HasOne(i => i.Event)
            .WithMany(u => u.EventEnrolments)
            .HasForeignKey(u => u.EventId)
            .IsRequired();
        builder.HasOne(i => i.EventEnrolmentStatus)
            .WithMany(u => u.EventEnrolments)
            .HasForeignKey(u => u.EventEnrolmentStatusId)
            .IsRequired(false);
        builder.HasOne(i => i.User)
            .WithMany(u => u.EventEnrolments)
            .HasForeignKey(u => u.UserId)
            .IsRequired();
    }
}

public class TicketEntityConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.HasKey(i => i.Id);
        builder.Property(i => i.DateStamp).IsRequired();
        builder.Property(i => i.Price).IsRequired();
        builder.Property(i => i.Description);
        builder.HasOne(i => i.Event)
            .WithMany(u => u.Tickets)
            .HasForeignKey(u => u.EventId)
            .IsRequired();
        builder.HasOne(i => i.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(u => u.UserId)
            .IsRequired(false);
        builder.HasOne(i => i.TicketCategory)
            .WithMany(u => u.Tickets)
            .HasForeignKey(u => u.TicketCategoryId)
            .IsRequired(false);
    }
}