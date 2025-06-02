namespace DavaiShodymo.EventEnrolments.CreateEnrolment;

public record CreateEnrolmentCommand(int EventId, int StatusId, bool IsFavorite);