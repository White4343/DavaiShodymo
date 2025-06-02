using System.Security.Claims;
using DavaiShodymo.EventEnrolmentStatuses;
using DavaiShodymo.Events;
using DavaiShodymo.JwtProviderHelper;

namespace DavaiShodymo.EventEnrolments.CreateEnrolment;

public class CreateEnrolmentHandler(IEventEnrolmentRepository eventEnrolmentRepository, 
    IEventRepository eventRepository, IEventEnrolmentStatusRepository eventEnrolmentStatusRepository)
{
    public async Task<EventEnrolmentResponse> HandleAsync(CreateEnrolmentCommand command, 
        ClaimsPrincipal user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var status = await eventEnrolmentStatusRepository.GetByIdAsync(command.StatusId, cancellationToken);

        if (status == null)
        {
            throw new ArgumentException($"Event enrolment status with ID {command.StatusId} does not exist.", nameof(command.StatusId));
        }

        var userId = GetUserInformation.GetUserIdFromClaims(user);

        var enrolment = new EventEnrolment(command.IsFavorite, command.EventId, command.StatusId, userId);

        var eventEntity = await eventRepository.GetByIdAsync(command.EventId, cancellationToken);

        if (eventEntity == null)
        {
            throw new ArgumentException($"Event with ID {command.EventId} does not exist.", nameof(command.EventId));
        }

        var enrolmentEntity = await eventEnrolmentRepository.CreateAsync(enrolment, cancellationToken);

        if (enrolmentEntity == null)
        {
            throw new InvalidOperationException("Failed to create event enrolment.");
        }

        EventEnrolmentResponse response = new EventEnrolmentResponse(enrolmentEntity.Id, 
            enrolmentEntity.UserId, enrolmentEntity.EventId, enrolmentEntity.EventEnrolmentStatusId, 
            status.Status, enrolmentEntity.DateStamp, enrolmentEntity.IsFavorite);

        return response;
    }
}