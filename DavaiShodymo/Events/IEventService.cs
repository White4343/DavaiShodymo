using DavaiShodymo.Events.GetEventById;

namespace DavaiShodymo.Events;

public interface IEventService
{
    Task<GetEventByIdResponse> GetEventByIdAsync(int eventId, CancellationToken cancellationToken);
}