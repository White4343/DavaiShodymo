using System.Security.Claims;
using DavaiShodymo.EndpointHelper;

namespace DavaiShodymo.EventReviews.UpdateReview;

public class UpdateReviewEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("reviews/update",
                async (HttpContext httpContext, UpdateReviewCommand request, UpdateReviewHandler useCase, ClaimsPrincipal user) =>
                    await useCase.HandleAsync(request, user, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Reviews")
            .RequireAuthorization()
            .WithOpenApi();
    }
}