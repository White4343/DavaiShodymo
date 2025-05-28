using System.Security.Claims;
using DavaiShodymo.EventFilters;
using DavaiShodymo.JwtProviderHelper;

namespace DavaiShodymo.Events.CreateEvent;

public class CreateEventHandler(IEventRepository eventRepository, IEventFilterRepository eventFilterRepository)
{
    public async Task HandleAsync(CreateEventCommand command, ClaimsPrincipal user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(eventRepository);

        var userId = GetUserInformation.GetUserIdFromClaims(user);

        Event newEvent = new Event(command.DateStart, command.DateEnd, command.Name, command.Description,
            command.Location, userId);

        Event result = await eventRepository.AddAsync(newEvent, cancellationToken).ConfigureAwait(false);

        if (command.Tags != null && command.Tags.Any())
        {
            foreach (var tagId in command.Tags)
            {
                var eventFilter = new EventFilter(tagId, null, result.Id);

                await eventFilterRepository.AddAsync(eventFilter, cancellationToken).ConfigureAwait(false);
            }
        }

        if (command.Categories != null && command.Categories.Any())
        {
            foreach (var categoryId in command.Categories)
            {
                var eventFilter = new EventFilter(null, categoryId, result.Id);

                await eventFilterRepository.AddAsync(eventFilter, cancellationToken).ConfigureAwait(false);
            }
        }
    }
}