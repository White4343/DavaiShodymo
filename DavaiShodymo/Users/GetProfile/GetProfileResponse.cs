namespace DavaiShodymo.Users.GetProfile;

public record GetProfileResponse(string UserName, string? FirstName, string? LastName,
    string? PhotoLink, DateTime? BirthDay, DateTime RegisterDate);