using DavaiShodymo.Data;
using Microsoft.EntityFrameworkCore;

namespace DavaiShodymo.EventReviews;

public class EventReviewRepository(AppDbContext) : IEventReviewRepository
{
    public async Task<float> GetAverageRatingAsync(int eventId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(eventId, nameof(eventId));
        var averageRating = await AppDbContext.EventReviews
            .Where(er => er.EventId == eventId)
            .AverageAsync(er => (float?)er.Rating, cancellationToken)
            .ConfigureAwait(false);
        return averageRating ?? 0f; // Return 0 if there are no reviews
    }
}