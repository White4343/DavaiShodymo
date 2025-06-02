namespace DavaiShodymo.EventEnrolmentStatuses;

public interface IEventEnrolmentStatusRepository
{
    Task<EventEnrolmentStatus> GetByIdAsync(int id, CancellationToken cancellationToken);
}