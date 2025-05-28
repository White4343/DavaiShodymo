namespace DavaiShodymo.Users.Register;

public record RegisterCommand(string UserName, string Email, string Password, 
    string? FirstName, string? LastName, string? PhotoLink, DateTime? BirthDate);