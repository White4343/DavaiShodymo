using DavaiShodymo.Data;
using Microsoft.EntityFrameworkCore;

namespace DavaiShodymo.EventImages;

public class EventImageRepository(AppDbContext context) : IEventImageRepository
{
    public async Task<EventImage?> AddAsync(EventImage eventImage, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(eventImage, nameof(eventImage));
        await context.EventImages.AddAsync(eventImage, cancellationToken).ConfigureAwait(false);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return eventImage;
    }

    public async Task UpdateAsync(EventImage eventImage, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(eventImage, nameof(eventImage));
        context.EventImages.Update(eventImage);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var eventImage = await context.EventImages.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);
        if (eventImage == null)
        {
            throw new KeyNotFoundException($"EventImage with ID {id} not found.");
        }
        context.EventImages.Remove(eventImage);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<EventImage?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        return await context.EventImages.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);
    }

    public async Task<IEnumerable<EventImage>> GetByEventIdAsync(int eventId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(eventId, nameof(eventId));
        return await context.EventImages
            .Where(ei => ei.EventId == eventId)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}