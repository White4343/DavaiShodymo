using DavaiShodymo.Events.GetEventById;

namespace DavaiShodymo.Events.GetEventsLibrary;

public class GetEventsLibraryHandler(IEventService eventService, IEventRepository eventRepository)
{
    public async Task<GetEventsLibraryResponse> HandleAsync(GetEventsLibraryCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var events = await eventRepository.GetEventsLibraryAsync(
            command.Name,
            command.DateStart,
            command.DateEnd,
            command.Location,
            command.UserId,
            command.PageNumber,
            command.PageSize,
            command.Tags,
            command.Categories,
            cancellationToken);

        List<GetEventByIdResponse> eventList = new List<GetEventByIdResponse>();

        foreach (var eventEntity in events)
        {
            var eventResponse = await eventService.GetEventByIdAsync(eventEntity.Id, cancellationToken);

            if (command.Rating is not null && eventResponse.TotalRating < command.Rating)
            {

            }
            else
            {
                eventList.Add(eventResponse);
            }
        }

        var response = new GetEventsLibraryResponse(command.PageNumber, command.PageSize, eventList.Count, eventList);

        return response;
    }
}