using DavaiShodymo.Data;
using DavaiShodymo.EventEnrolments;
using Microsoft.EntityFrameworkCore;

namespace DavaiShodymo.EventEnrolmentStatuses;

public class EventEnrolmentRepository(AppDbContext context) : IEventEnrolmentRepository
{
    public async Task<EventEnrolment?> CreateAsync(EventEnrolment status, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(status);

        await context.EventEnrolments.AddAsync(status, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return status;
    }

    public async Task<EventEnrolment?> UpdateAsync(EventEnrolment status, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(status);
        context.EventEnrolments.Update(status);
        await context.SaveChangesAsync(cancellationToken);
        return status;
    }

    public async Task<EventEnrolment?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));
        return await context.EventEnrolments.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<int> GetTotalInterestedByEventIdAsync(int eventId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(eventId, nameof(eventId));
        return await context.EventEnrolments
            .CountAsync(e => e.EventId == eventId && e.EventEnrolmentStatus.Status == "Interested", cancellationToken);
    }

    public async Task<List<EventEnrolment>?> GetByUserAsync(int userId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(userId, nameof(userId));

        return await context.EventEnrolments
            .Where(e => e.UserId == userId)
            .Include(e => e.EventEnrolmentStatus)
            .ToListAsync(cancellationToken);
    }
}