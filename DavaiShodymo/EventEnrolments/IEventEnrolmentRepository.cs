using DavaiShodymo.EventEnrolments;

namespace DavaiShodymo.EventEnrolmentStatuses;

public interface IEventEnrolmentRepository
{
    Task<EventEnrolment?> CreateAsync(EventEnrolment status, CancellationToken cancellationToken);
    Task<EventEnrolment?> UpdateAsync(EventEnrolment status, CancellationToken cancellationToken);
    Task<EventEnrolment?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> GetTotalInterestedByEventIdAsync(int eventId, CancellationToken cancellationToken);
    Task<List<EventEnrolment>?> GetByUserAsync(int userId, CancellationToken cancellationToken);
}