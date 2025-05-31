using DavaiShodymo.Users;

namespace DavaiShodymo.EventReviews.GetReviewByEventId;

public class GetReviewByEventIdHandler(IEventReviewRepository eventReviewRepository, 
    IUserRepository userRepository)
{
    public async Task<GetReviewByEventIdResponse> HandleAsync(GetReviewByEventIdCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        var eventReviews = await eventReviewRepository.GetByEventIdAsync(command.EventId, command.PageNumber, command.PageSize, cancellationToken)
            .ConfigureAwait(false);

        List<EventReviewResponse> reviewResponses = new List<EventReviewResponse>();

        foreach (var review in eventReviews)
        {
            var user = await userRepository.GetByIdAsync(review.UserId, cancellationToken)
                .ConfigureAwait(false);

            var userFullName = user != null ? $"{user.FirstName} {user.LastName}" : "Unknown User";

            reviewResponses.Add(new EventReviewResponse(review.EventId, review.Description, review.Rating, 
                review.DateStamp, review.EventId, review.UserId, userFullName));
        }

        var response = new GetReviewByEventIdResponse(reviewResponses, command.PageNumber, command.PageSize, eventReviews.Count);

        return response;
    }
}