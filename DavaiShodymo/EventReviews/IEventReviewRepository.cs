namespace DavaiShodymo.EventReviews;

public interface IEventReviewRepository
{
    Task<float> GetAverageRatingAsync(int eventId, CancellationToken cancellationToken);
    Task<EventReview?> AddAsync(EventReview eventReview, CancellationToken cancellationToken);
    Task<EventReview?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<EventReview?> UpdateAsync(EventReview eventReview, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<List<EventReview>> GetByEventIdAsync(int eventId, int pageNumber, int pageSize, CancellationToken cancellationToken);
}