namespace DavaiShodymo.Users.UpdateUser;

public record UpdateUserCommand(int Id, string? FirstName, string? LastName, string? PhotoLink, DateTime? BirthDate);