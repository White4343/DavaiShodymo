using DavaiShodymo.Events;
using DavaiShodymo.Users;

namespace DavaiShodymo.EventReviews;

public class EventReview(string description, int rating, int eventId, int userId)
{
    public int Id { get; set; }
    public string Description { get; set; } = description;
    public int Rating { get; set; } = rating;
    public DateTime DateStamp { get; set; } = DateTime.UtcNow;
    public int EventId { get; set; } = eventId;
    public Event Event { get; set; } = null!;
    public int UserId { get; set; } = userId;
    public User User { get; set; } = null!;

    public void Update(string description, int rating)
    {
        Description = description;
        Rating = rating;
        DateStamp = DateTime.UtcNow;
    }
}