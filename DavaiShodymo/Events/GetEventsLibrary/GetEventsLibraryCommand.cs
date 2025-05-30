namespace DavaiShodymo.Events.GetEventsLibrary;

public record GetEventsLibraryCommand(string? Name, DateTime? DateStart, DateTime? DateEnd,
    string? Location, int? UserId, int PageNumber, int PageSize, List<int?>? Tags, List<int?>? Categories);