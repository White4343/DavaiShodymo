namespace DavaiShodymo.EventReviews.CreateReview;

public record CreateReviewCommand(string Description, int Rating, int EventId);