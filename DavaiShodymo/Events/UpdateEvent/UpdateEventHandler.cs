using System.Security.Claims;
using DavaiShodymo.JwtProviderHelper;
using DavaiShodymo.Roles;

namespace DavaiShodymo.Events.UpdateEvent;

public class UpdateEventHandler(IEventRepository eventRepository) 
{
    public async Task HandleAsync(UpdateEventCommand command, ClaimsPrincipal user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var existingEvent = await eventRepository.GetByIdAsync(command.Id, cancellationToken);

        if (existingEvent == null)
        {
            throw new ArgumentNullException($"Event with ID {command.Id} not found");
        }

        var userId = GetUserInformation.GetUserIdFromClaims(user);

        var roleId = GetUserInformation.GetRoleIdFromClaims(user);

        if (userId != existingEvent.UserId && roleId != (int)RolesEnum.Admin)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this event.");
        }

        existingEvent.Update(command.DateStart, command.DateEnd, command.Name, command.Description, 
            command.Location);

        await eventRepository.UpdateAsync(existingEvent, cancellationToken).ConfigureAwait(false);
    }
}