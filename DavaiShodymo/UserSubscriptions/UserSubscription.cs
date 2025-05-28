using DavaiShodymo.Subscriptions;
using DavaiShodymo.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace DavaiShodymo.UserSubscriptions;

public class UserSubscription
{
    public UserSubscription() { }

    public UserSubscription(int dateRange, float? price, int userId, int subscriptionId)
    {
        DateStart = DateTime.UtcNow;
        DateEnd = DateStart.AddDays(dateRange);
        Price = price;
        UserId = userId;
        SubscriptionId = subscriptionId;
        IsActive = true;
        DateStamp = DateTime.UtcNow;
    }

    public int Id { get; set; }
    public DateTime DateStart { get; set; } 
    public DateTime DateEnd { get; set; }
    public DateTime DateStamp { get; set; }
    public float? Price { get; set; } 
    public bool IsActive { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }

    public void Update(int dateRange, float? price, DateTime dateEnd)
    {
        DateStart = DateTime.UtcNow;
        DateEnd = dateEnd;
        Price = price;
        IsActive = true;
        DateStamp = DateTime.UtcNow;
    }

    public void UpdateIsActive()
    {
        IsActive = DateEnd > DateTime.UtcNow;
        DateStamp = DateTime.UtcNow;
    }
}