using DavaiShodymo.Categories;
using DavaiShodymo.EventFilters;

namespace DavaiShodymo.Tags;

public class Tag(string name, int categoryId)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public int CategoryId { get; set; } = categoryId;
    public Category Category { get; set; } = null!;
    public ICollection<EventFilter> EventFilters { get; set; } = new List<EventFilter>();
}