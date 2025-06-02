using DavaiShodymo.EventEnrolmentStatuses;
using DavaiShodymo.Users;

namespace DavaiShodymo.EventEnrolments.GetEnrolmentByUser;

public class GetEnrolmentByUserHandler(IUserRepository userRepository, 
    IEventEnrolmentRepository eventEnrolmentRepository, IEventEnrolmentStatusRepository eventEnrolmentStatus)
{
    public async Task<List<EventEnrolmentResponse>> HandleAsync(int userId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(userId, nameof(userId));

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);

        if (user == null)
        {
            throw new ArgumentException($"User with ID {userId} does not exist.", nameof(userId));
        }
        var enrolments = await eventEnrolmentRepository.GetByUserAsync(userId, cancellationToken);

        if (enrolments == null || !enrolments.Any())
        {
            return new List<EventEnrolmentResponse>();
        }
        var responses = new List<EventEnrolmentResponse>();

        foreach (var enrolment in enrolments)
        {
            var status = await eventEnrolmentStatus.GetByIdAsync(enrolment.EventEnrolmentStatusId, cancellationToken);
            responses.Add(new EventEnrolmentResponse(enrolment.Id, enrolment.UserId, enrolment.EventId,
                enrolment.EventEnrolmentStatusId, status.Status, enrolment.DateStamp, enrolment.IsFavorite));
        }
        return responses;
    }
}