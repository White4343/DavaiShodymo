namespace DavaiShodymo.Events.UpdateEvent;

public record UpdateEventCommand(int Id, string? Name, DateTime? DateStart, DateTime? DateEnd, bool? IsActive, string? Description, string? Location, List<int>? Tags, List<int>? Categories);