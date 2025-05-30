namespace DavaiShodymo.EventImages;

public interface IEventImageRepository
{
    Task<EventImage?> AddAsync(EventImage eventImage, CancellationToken cancellationToken);
    Task UpdateAsync(EventImage eventImage, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<EventImage?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<EventImage>> GetByEventIdAsync(int eventId, CancellationToken cancellationToken);
}