using System.Security.Claims;
using DavaiShodymo.Events;
using DavaiShodymo.JwtProviderHelper;
using DavaiShodymo.Users;

namespace DavaiShodymo.EventReviews.CreateReview;

public class CreateReviewHandler(IEventRepository eventRepository, 
    IEventReviewRepository eventReviewRepository, IUserRepository userRepository)
{
    public async Task<EventReviewResponse> HandleAsync(CreateReviewCommand command, ClaimsPrincipal user,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        var eventEntity = await eventRepository.GetByIdAsync(command.EventId, cancellationToken)
            .ConfigureAwait(false);

        if (eventEntity == null)
        {
            throw new ArgumentException($"Event with ID {command.EventId} not found.", 
                nameof(command.EventId));
        }

        var userId = GetUserInformation.GetUserIdFromClaims(user);

        var eventReview = new EventReview(command.Description, command.Rating, command.EventId, userId);

        var createdReview = await eventReviewRepository.AddAsync(eventReview, cancellationToken)
            .ConfigureAwait(false);

        if (createdReview == null)
        {
            throw new InvalidOperationException("Failed to create event review.");
        }

        var userEntity = await userRepository.GetByIdAsync(userId, cancellationToken)
            .ConfigureAwait(false);

        var userFullName = userEntity != null
            ? $"{userEntity.FirstName} {userEntity.LastName}"
            : "Unknown User";

        var result = new EventReviewResponse(createdReview.EventId, createdReview.Description, 
            createdReview.Rating, createdReview.DateStamp, createdReview.EventId, createdReview.UserId, 
            userFullName);

        return result;
    }
}