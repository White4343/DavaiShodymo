using DavaiShodymo.Events;
using DavaiShodymo.Logs;
using DavaiShodymo.Tickets;

namespace DavaiShodymo.TicketCategories;

public class TicketCategory(float price, int amount, string name, string? description, 
    bool isActive, string? externalPricingLink, int eventId)
{
    public int Id { get; set; }
    public float Price { get; set; } = price;
    public int Amount { get; set; } = amount;
    public string Name { get; set; } = name;
    public string? Description { get; set; } = description;
    public bool IsActive { get; set; } = isActive;
    public string? ExternalPricingLink { get; set; } = externalPricingLink;
    public DateTime DateStamp { get; set; } = DateTime.UtcNow;
    public int EventId { get; set; } = eventId;
    public Event Event { get; set; } = null!;
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    public ICollection<LogStripe> LogStripes { get; set; } = new List<LogStripe>();


    public void Update(float price, int amount, string name, string? description, 
        bool isActive, string? externalPricingLink)
    {
        Price = price;
        Amount = amount;
        Name = name;
        Description = description;
        IsActive = isActive;
        ExternalPricingLink = externalPricingLink;
        DateStamp = DateTime.UtcNow;
    }
}