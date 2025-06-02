using System.Security.Claims;
using DavaiShodymo.EventEnrolmentStatuses;
using DavaiShodymo.JwtProviderHelper;
using DavaiShodymo.Roles;

namespace DavaiShodymo.EventEnrolments.UpdateEnrolment;

public class UpdateEnrolmentHandler(IEventEnrolmentRepository eventEnrolmentRepository)
{
    public async Task<EventEnrolmentResponse> HandleAsync(UpdateEnrolmentCommand command,
        ClaimsPrincipal user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var enrolment = await eventEnrolmentRepository.GetByIdAsync(command.Id, cancellationToken);

        if (enrolment == null)
        {
            throw new ArgumentException($"Event enrolment with ID {command.Id} does not exist.", nameof(command.Id));
        }

        var userId = GetUserInformation.GetUserIdFromClaims(user);

        var roleId = GetUserInformation.GetRoleIdFromClaims(user);

        if (enrolment.UserId != userId && roleId != (int)RolesEnum.Admin)
        {
            throw new UnauthorizedAccessException("You do not have permission to update this enrolment.");
        }

        enrolment.Update(command.IsFavorite, command.StatusId);

        var updatedEnrolment = await eventEnrolmentRepository.UpdateAsync(enrolment, cancellationToken);

        if (updatedEnrolment == null)
        {
            throw new InvalidOperationException("Failed to update event enrolment.");
        }

        EventEnrolmentResponse response = new EventEnrolmentResponse(updatedEnrolment.Id,
            updatedEnrolment.UserId, updatedEnrolment.EventId, updatedEnrolment.EventEnrolmentStatusId,
            updatedEnrolment.EventEnrolmentStatus.Status, updatedEnrolment.DateStamp, updatedEnrolment.IsFavorite);

        return response;
    }
}