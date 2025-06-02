namespace DavaiShodymo.EventEnrolments.UpdateEnrolment;

public record UpdateEnrolmentCommand(int Id, int? StatusId, bool? IsFavorite);