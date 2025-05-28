using DavaiShodymo.EventEnrolments;
using DavaiShodymo.EventFilters;
using DavaiShodymo.EventImages;
using DavaiShodymo.EventReviews;
using DavaiShodymo.TicketCategories;
using DavaiShodymo.Tickets;
using DavaiShodymo.Users;

namespace DavaiShodymo.Events;

public class Event(DateTime dateStart, DateTime dateEnd, string name, string? description, string? location, int userId)
{
    public int Id { get; set; }
    public DateTime DateStart { get; set; } = dateStart;
    public DateTime DateEnd { get; set; } = dateEnd;
    public DateTime DateStamp { get; set; } = DateTime.UtcNow;
    public string Name { get; set; } = name;
    public string? Description { get; set; } = description;
    public string? Location { get; set; } = location;
    public bool IsActive { get; set; } = dateStart >= DateTime.UtcNow && dateEnd <= DateTime.UtcNow;
    public int UserId { get; set; } = userId;
    public User User { get; set; } = null!;
    public ICollection<EventImage> EventImages { get; set; } = new List<EventImage>();
    public ICollection<EventReview> EventReviews { get; set; } = new List<EventReview>();
    public ICollection<EventFilter> EventFilters { get; set; } = new List<EventFilter>();
    public ICollection<TicketCategory> TicketCategories { get; set; } = new List<TicketCategory>();
    public ICollection<EventEnrolment> EventEnrolments { get; set; } = new List<EventEnrolment>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public void Update(DateTime dateStart, DateTime dateEnd, string name, string? description, string? location)
    {
        DateStart = dateStart;
        DateEnd = dateEnd;
        Name = name;
        Description = description;
        Location = location;
        IsActive = dateStart >= DateTime.UtcNow && dateEnd <= DateTime.UtcNow;
        DateStamp = DateTime.UtcNow;
    }
}