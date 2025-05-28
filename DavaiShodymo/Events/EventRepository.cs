using DavaiShodymo.Data;
using Microsoft.EntityFrameworkCore;

namespace DavaiShodymo.Events;

public class EventRepository(AppDbContext context, ILogger<EventRepository> logger) : IEventRepository
{
    public async Task<Event?> AddAsync(Event eventEntity, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(context.Events);
            ArgumentNullException.ThrowIfNull(eventEntity);
            var newEvent = await context.Events.AddAsync(eventEntity, cancellationToken).ConfigureAwait(false);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("Event {EventName} created", eventEntity.Name);

            return newEvent.Entity;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error creating event {EventName}", eventEntity.Name);
            throw;
        }
    }

    public async Task UpdateAsync(Event eventEntity, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(context.Events);
            ArgumentNullException.ThrowIfNull(eventEntity);
            context.Events.Update(eventEntity);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("Event {EventName} updated", eventEntity.Name);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating event {EventName}", eventEntity.Name);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(context.Events);
            var eventEntity = await context.Events.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);
            if (eventEntity == null)
            {
                logger.LogWarning("Event with ID {Id} not found", id);
                throw new ArgumentNullException($"Event with ID {id} not found");
            }
            context.Events.Remove(eventEntity);
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("Event with ID {Id} deleted", id);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error deleting event with ID {Id}", id);
            throw;
        }
    }

    public async Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(context.Events);

            var eventEntity = await context.Events.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);

            if (eventEntity == null)
            {
                logger.LogWarning("Event with ID {Id} not found", id);
                throw new ArgumentNullException($"Event with ID {id} not found");
            }

            logger.LogInformation("Event with ID {Id} found", id);

            return eventEntity;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error retrieving event with ID {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(context.Events);
            var events = await context.Events.ToListAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("Retrieved {Count} events", events.Count);
            return events;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error retrieving all events");
            throw;
        }
    }

    public async Task<IEnumerable<Event>> GetEventsLibraryAsync(string? name, DateTime? dateStart, DateTime? dateEnd, string? location, int? userId,
        int pageNumber, int pageSize, List<int?>? tags, List<int?>? categories, CancellationToken cancellationToken)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(context.Events);
            var query = context.Events.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            if (dateStart.HasValue)
            {
                query = query.Where(e => e.DateStart >= dateStart.Value);
            }
            if (dateEnd.HasValue)
            {
                query = query.Where(e => e.DateEnd <= dateEnd.Value);
            }
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(e => e.Location.Contains(location, StringComparison.OrdinalIgnoreCase));
            }
            if (userId.HasValue)
            {
                query = query.Where(e => e.UserId == userId.Value);
            }
            if (tags != null && tags.Count > 0)
            {
                query = query.Where(e => e.EventFilters.Any(f => tags.Contains(f.TagId)));
            }
            if (categories != null && categories.Count > 0)
            {
                query = query.Where(e => e.TicketCategories.Any(c => categories.Contains(c.Id)));
            }

            var events = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
            logger.LogInformation("Retrieved {Count} events for library", events.Count);
            return events;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error retrieving events for library");
            throw;
        }
    }
}