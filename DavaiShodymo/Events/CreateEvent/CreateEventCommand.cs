namespace DavaiShodymo.Events.CreateEvent;

public record CreateEventCommand(string Name, DateTime DateStart, DateTime DateEnd, bool IsActive, string? Description, string? Location, List<int>? Tags, List<int>? Categories);