namespace DavaiShodymo.EventImagePlacements;

public interface IEventImagePlacementRepository
{
    Task<List<EventImagePlacement>> GetAllAsync(CancellationToken cancellationToken);
}