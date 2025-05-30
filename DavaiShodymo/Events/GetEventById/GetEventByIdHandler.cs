namespace DavaiShodymo.Events.GetEventById;

public class GetEventByIdHandler(IEventService eventService)
{
    public async Task<GetEventByIdResponse> HandleAsync(GetEventByIdCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var result = await eventService.GetEventByIdAsync(command.Id, cancellationToken).ConfigureAwait(false);

        return result;
    }
}