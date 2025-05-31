using System.Security.Claims;
using DavaiShodymo.EndpointHelper;

namespace DavaiShodymo.EventReviews.CreateReview;

public class CreateReviewEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("reviews/create",
                async (HttpContext httpContext, CreateReviewCommand request, CreateReviewHandler useCase, ClaimsPrincipal user) =>
                    await useCase.HandleAsync(request, user, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Reviews")
            .RequireAuthorization()
            .WithOpenApi();
    }
}