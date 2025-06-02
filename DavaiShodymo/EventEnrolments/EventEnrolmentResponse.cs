namespace DavaiShodymo.EventEnrolments;

public record EventEnrolmentResponse(int Id, int UserId, int EventId, int StatusId, string Status,
    DateTime DateStamp, bool IsFavorite);