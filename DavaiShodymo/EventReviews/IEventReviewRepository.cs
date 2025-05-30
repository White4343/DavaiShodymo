namespace DavaiShodymo.EventReviews;

public interface IEventReviewRepository
{
    Task<float> GetAverageRatingAsync(int eventId, CancellationToken cancellationToken);
}