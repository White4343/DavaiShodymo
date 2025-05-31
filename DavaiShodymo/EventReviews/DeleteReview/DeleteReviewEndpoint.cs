using System.Security.Claims;
using DavaiShodymo.EndpointHelper;

namespace DavaiShodymo.EventReviews.DeleteReview;

public class DeleteReviewEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("reviews/delete/{id}",
                async (HttpContext httpContext, int id, DeleteReviewHandler useCase, ClaimsPrincipal user) =>
                    await useCase.HandleAsync(id, user, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Reviews")
            .RequireAuthorization()
            .WithOpenApi();
    }
}