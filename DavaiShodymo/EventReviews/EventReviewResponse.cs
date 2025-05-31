namespace DavaiShodymo.EventReviews;

public record EventReviewResponse(int Id, string Description, int Rating, DateTime DateStamp, int EventId,
    int UserId, string UserFullName);