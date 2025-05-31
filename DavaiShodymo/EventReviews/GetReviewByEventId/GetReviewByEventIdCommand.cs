namespace DavaiShodymo.EventReviews.GetReviewByEventId;

public record GetReviewByEventIdCommand(int EventId, int PageNumber, int PageSize);