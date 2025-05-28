namespace DavaiShodymo.EventFilters;

public interface IEventFilterRepository
{
    Task AddAsync(EventFilter eventFilter, CancellationToken cancellationToken);
}