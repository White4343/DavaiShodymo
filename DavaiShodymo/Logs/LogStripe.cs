using DavaiShodymo.Subscriptions;
using DavaiShodymo.TicketCategories;

namespace DavaiShodymo.Logs
{
    public class LogStripe(float? price, int? ticketCategoryId, int? subscriptionId, int logId)
    {
        public int Id { get; set; }
        public float? Price { get; set; } = price;
        public int? TicketCategoryId { get; set; } = ticketCategoryId;
        public TicketCategory? TicketCategory { get; set; }
        public int? SubscriptionId { get; set; } = subscriptionId;
        public Subscription? Subscription { get; set; }
        public int LogId { get; set; } = logId;
        public Log Log { get; set; } = null!;
    }
}