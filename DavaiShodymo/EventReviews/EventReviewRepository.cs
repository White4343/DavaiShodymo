using DavaiShodymo.Data;
using Microsoft.EntityFrameworkCore;

namespace DavaiShodymo.EventReviews;

public class EventReviewRepository(AppDbContext context) : IEventReviewRepository
{
    public async Task<float> GetAverageRatingAsync(int eventId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(eventId, nameof(eventId));
        var averageRating = await context.EventReviews
            .Where(er => er.EventId == eventId)
            .AverageAsync(er => (float?)er.Rating, cancellationToken)
            .ConfigureAwait(false);
        return averageRating ?? 0f; // Return 0 if there are no reviews
    }

    public async Task<EventReview?> AddAsync(EventReview eventReview, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(eventReview, nameof(eventReview));
        // Validate the event review before adding
        if (eventReview.Rating < 1 || eventReview.Rating > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(eventReview.Rating), "Rating must be between 1 and 5.");
        }
        await context.EventReviews.AddAsync(eventReview, cancellationToken);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return eventReview;
    }

    public async Task<EventReview?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        var eventReview = await context.EventReviews
            .FirstOrDefaultAsync(er => er.Id == id, cancellationToken)
            .ConfigureAwait(false);
        return eventReview;
    }

    public async Task<EventReview?> UpdateAsync(EventReview eventReview, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(eventReview, nameof(eventReview));
        // Validate the event review before updating
        if (eventReview.Rating < 1 || eventReview.Rating > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(eventReview.Rating), "Rating must be between 1 and 5.");
        }
        context.EventReviews.Update(eventReview);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return eventReview;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        var eventReview = await GetByIdAsync(id, cancellationToken).ConfigureAwait(false);
        if (eventReview == null)
        {
            return false; // Review not found
        }
        context.EventReviews.Remove(eventReview);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    public async Task<List<EventReview>> GetByEventIdAsync(int eventId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(eventId, nameof(eventId));
        ArgumentNullException.ThrowIfNull(pageNumber, nameof(pageNumber));
        ArgumentNullException.ThrowIfNull(pageSize, nameof(pageSize));

        var eventReviews = await context.EventReviews
            .Where(er => er.EventId == eventId)
            .OrderByDescending(er => er.DateStamp)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        return eventReviews;
    }
}