using DavaiShodymo.Data;

namespace DavaiShodymo.EventFilters;

public class EventFilterRepository(AppDbContext context, ILogger<EventFilterRepository> logger) : IEventFilterRepository
{
    public async Task AddAsync(EventFilter eventFilter, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(context.EventFilters);
            ArgumentNullException.ThrowIfNull(eventFilter);
            var newEventFilter = await context.EventFilters.AddAsync(eventFilter, cancellationToken).ConfigureAwait(false);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("Event filter {Id} created", eventFilter.Id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating event filter {EventId}", eventFilter.EventId);
            throw;
        }
    }
}