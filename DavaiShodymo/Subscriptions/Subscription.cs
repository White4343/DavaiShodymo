using DavaiShodymo.Logs;
using DavaiShodymo.Users;
using DavaiShodymo.UserSubscriptions;

namespace DavaiShodymo.Subscriptions;

public class Subscription(int dateRange, string name, float? price)
{
    public int Id { get; set; }
    public int DateRange { get; set; } = dateRange; // in days
    public string Name { get; set; } = name;
    public float? Price { get; set; } = price;
    public DateTime DateStamp { get; set; } = DateTime.UtcNow;
    public ICollection<LogStripe> LogStripes { get; set; } = new List<LogStripe>();
    public ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();

    public void Update(int dateRange, string name, float? price)
    {
        DateRange = dateRange;
        Name = name;
        Price = price;
        DateStamp = DateTime.UtcNow;
    }
}