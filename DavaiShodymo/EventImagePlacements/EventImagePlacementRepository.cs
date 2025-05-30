using DavaiShodymo.Data;
using Microsoft.EntityFrameworkCore;

namespace DavaiShodymo.EventImagePlacements;

public class EventImagePlacementRepository(AppDbContext context) : IEventImagePlacementRepository
{
    public async Task<List<EventImagePlacement>> GetAllAsync(CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        return await context.EventImagePlacements
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}