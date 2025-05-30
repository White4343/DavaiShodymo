using DavaiShodymo.Events.GetEventById;

namespace DavaiShodymo.Events.GetEventsLibrary;

public record GetEventsLibraryResponse(int PageNum, int PageSize, int TotalCount, List<GetEventByIdResponse> Events);