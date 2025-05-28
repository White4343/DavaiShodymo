using DavaiShodymo.EventImagePlacements;
using DavaiShodymo.Events;

namespace DavaiShodymo.EventImages;

public class EventImage(string photoLink, int eventImagePlacementId, int eventId)
{
    public int Id { get; set; }
    public string PhotoLink { get; set; } = photoLink;
    public int EventImagePlacementId { get; set; } = eventImagePlacementId;
    public EventImagePlacement EventImagePlacement { get; set; } = null!;
    public int EventId { get; set; } = eventId;
    public Event Event { get; set; } = null!;

    public void Update(string photoLink)
    {
        PhotoLink = photoLink;
    }
}