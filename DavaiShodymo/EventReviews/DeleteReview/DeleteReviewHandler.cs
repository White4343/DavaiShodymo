using System.Security.Claims;
using DavaiShodymo.JwtProviderHelper;
using DavaiShodymo.Roles;

namespace DavaiShodymo.EventReviews.DeleteReview;

public class DeleteReviewHandler(IEventReviewRepository eventReviewRepository)
{
    public async Task<bool> HandleAsync(int reviewId, ClaimsPrincipal user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(reviewId, nameof(reviewId));

        var eventReview = await eventReviewRepository.GetByIdAsync(reviewId, cancellationToken)
            .ConfigureAwait(false);

        if (eventReview == null)
        {
            throw new ArgumentException($"Review with ID {reviewId} not found.", nameof(reviewId));
        }

        var userId = GetUserInformation.GetUserIdFromClaims(user);
        var roleId = GetUserInformation.GetRoleIdFromClaims(user);

        if (eventReview.UserId != userId && roleId != (int)RolesEnum.Admin)
        {
            throw new UnauthorizedAccessException("You are not authorized to delete this review.");
        }

        var isDeleted = await eventReviewRepository.DeleteAsync(reviewId, cancellationToken)
            .ConfigureAwait(false);

        if (!isDeleted)
        {
            throw new InvalidOperationException($"Failed to delete review with ID {reviewId}.");
        }

        return true; 
    }
}