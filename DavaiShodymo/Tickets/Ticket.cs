using DavaiShodymo.Events;
using DavaiShodymo.TicketCategories;
using DavaiShodymo.Users;

namespace DavaiShodymo.Tickets;

public class Ticket(float price, string? description, int eventId, int userId, int ticketCategoryId)
{
    public int Id { get; set; }
    public float Price { get; set; } = price;
    public string? Description { get; set; } = description;
    public DateTime DateStamp { get; set; } = DateTime.UtcNow;
    public int EventId { get; set; } = eventId;
    public Event Event { get; set; } = null!;
    public int UserId { get; set; } = userId;
    public User User { get; set; } = null!;
    public int TicketCategoryId { get; set; } = ticketCategoryId;
    public TicketCategory TicketCategory { get; set; } = null!;
}