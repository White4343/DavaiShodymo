using DavaiShodymo.EndpointHelper;

namespace DavaiShodymo.EventReviews.GetReviewByEventId;

public class GetReviewByEventIdHandlerEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("reviews/getbyevent",
                async (HttpContext httpContext, GetReviewByEventIdCommand command, GetReviewByEventIdHandler useCase) =>
                    await useCase.HandleAsync(command, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Reviews")
            .WithOpenApi();
    }
}