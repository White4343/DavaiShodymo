namespace DavaiShodymo.EventReviews.GetReviewByEventId;

public record GetReviewByEventIdResponse(List<EventReviewResponse>? EventReviews, int PageNum, int PageSize, int TotalCount);