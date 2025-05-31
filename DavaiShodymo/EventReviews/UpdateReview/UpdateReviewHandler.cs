using System.Security.Claims;
using DavaiShodymo.JwtProviderHelper;
using DavaiShodymo.Roles;
using DavaiShodymo.Users;

namespace DavaiShodymo.EventReviews.UpdateReview;

public class UpdateReviewHandler(IUserRepository userRepository, 
    IEventReviewRepository eventReviewRepository)
{
    public async Task<EventReviewResponse> HandleAsync(UpdateReviewCommand command, ClaimsPrincipal user,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        var eventReview = await eventReviewRepository.GetByIdAsync(command.Id, cancellationToken)
            .ConfigureAwait(false);
        
        if (eventReview == null)
        {
            throw new ArgumentException($"Review with ID {command.Id} not found.", nameof(command.Id));
        }

        var userId = GetUserInformation.GetUserIdFromClaims(user);
        var roleId = GetUserInformation.GetRoleIdFromClaims(user);

        if (eventReview.UserId != userId && roleId != (int)RolesEnum.Admin)
        {
            throw new UnauthorizedAccessException("You are not authorized to update this review.");
        }

        eventReview.Update(command.Description, command.Rating);

        var updatedReview = await eventReviewRepository.UpdateAsync(eventReview, cancellationToken)
            .ConfigureAwait(false);

        if (updatedReview == null)
        {
            throw new InvalidOperationException("Failed to update event review.");
        }

        var userEntity = await userRepository.GetByIdAsync(userId, cancellationToken)
            .ConfigureAwait(false);

        var userFullName = userEntity != null
            ? $"{userEntity.FirstName} {userEntity.LastName}"
            : "Unknown User";

        var result = new EventReviewResponse(updatedReview.EventId, updatedReview.Description,
            updatedReview.Rating, updatedReview.DateStamp, updatedReview.EventId, updatedReview.UserId,
            userFullName);

        return result;
    }
}