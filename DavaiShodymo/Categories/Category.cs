using DavaiShodymo.EventFilters;
using DavaiShodymo.Tags;

namespace DavaiShodymo.Categories;

public class Category(string name)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<EventFilter> EventFilters { get; set; } = new List<EventFilter>();
}