namespace DavaiShodymo.Events;

public interface IEventRepository
{
    Task<Event?> AddAsync(Event eventEntity, CancellationToken cancellationToken);
    Task UpdateAsync(Event eventEntity, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Event>> GetEventsLibraryAsync(string? name, DateTime? dateStart, DateTime? dateEnd, 
        string? location, int? userId, int pageNumber, int pageSize, List<int?>? tags, List<int?>? categories, 
        CancellationToken cancellationToken);

}