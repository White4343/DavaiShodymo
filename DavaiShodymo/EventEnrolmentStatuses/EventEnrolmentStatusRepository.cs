using DavaiShodymo.Data;

namespace DavaiShodymo.EventEnrolmentStatuses;

public class EventEnrolmentStatusRepository(AppDbContext context) : IEventEnrolmentStatusRepository
{
    public async Task<EventEnrolmentStatus> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));
        
        var status = await context.EventEnrolmentStatuses.FindAsync(new object[] { id }, cancellationToken);
        
        if (status == null)
        {
            throw new KeyNotFoundException($"Event enrolment status with ID {id} not found.");
        }
        
        return status;
    }
}