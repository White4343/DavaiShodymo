using DavaiShodymo.Bans;
using DavaiShodymo.Categories;
using DavaiShodymo.Data.EntityConfigurations;
using DavaiShodymo.EventEnrolments;
using DavaiShodymo.EventEnrolmentStatuses;
using DavaiShodymo.EventFilters;
using DavaiShodymo.EventImagePlacements;
using DavaiShodymo.EventImages;
using DavaiShodymo.EventReviews;
using DavaiShodymo.Events;
using DavaiShodymo.Logs;
using DavaiShodymo.Roles;
using DavaiShodymo.Subscriptions;
using DavaiShodymo.Tags;
using DavaiShodymo.TicketCategories;
using DavaiShodymo.Tickets;
using DavaiShodymo.Users;
using DavaiShodymo.UserSubscriptions;
using Microsoft.EntityFrameworkCore;

namespace DavaiShodymo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EventEnrolmentStatus>? EventEnrolmentStatuses { get; set; }
        public DbSet<EventImagePlacement>? EventImagePlacements { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<Subscription>? Subscriptions { get; set; }
        public DbSet<LogType>? LogTypes { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Ban>? Bans { get; set; }
        public DbSet<UserSubscription>? UserSubscriptions { get; set; }
        public DbSet<Log>? Logs { get; set; }
        public DbSet<Event>? Events { get; set; }
        public DbSet<TicketCategory>? TicketCategories { get; set; }
        public DbSet<LogStripe>? LogStripes { get; set; }
        public DbSet<EventImage>? EventImages { get; set; }
        public DbSet<EventFilter>? EventFilters { get; set; }
        public DbSet<EventReview>? EventReviews { get; set; }
        public DbSet<EventEnrolment>? EventEnrolments { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            modelBuilder.ApplyConfiguration(new EventEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventImagePlacementEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TagEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LogTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BanEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserSubscriptionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LogEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TicketCategoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LogStripeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventImageEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventFilterEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventReviewEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventEnrolmentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TicketEntityConfiguration());
        }
    }
}