using DavaiShodymo.Categories;
using DavaiShodymo.Events;
using DavaiShodymo.Tags;

namespace DavaiShodymo.EventFilters;

public class EventFilter(int? tagId, int? categoryId, int eventId)
{
    public int Id { get; set; }
    public int? TagId { get; set; } = tagId;
    public Tag? Tag { get; set; }
    public int? CategoryId { get; set; } = categoryId;
    public Category? Category { get; set; }
    public int EventId { get; set; } = eventId;
    public Event Event { get; set; } = null!;
}