using DavaiShodymo.EventImages;

namespace DavaiShodymo.EventImagePlacements;

public class EventImagePlacement(string placement)
{
    public int Id { get; set; }
    public string Placement { get; set; } = placement;
    public ICollection<EventImage> EventImages { get; set; } = new List<EventImage>();
}