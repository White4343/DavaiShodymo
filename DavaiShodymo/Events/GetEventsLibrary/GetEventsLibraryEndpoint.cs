using DavaiShodymo.EndpointHelper;

namespace DavaiShodymo.Events.GetEventsLibrary;

public class GetEventsLibraryEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events/get",
                async (HttpContext httpContext, GetEventsLibraryCommand request, GetEventsLibraryHandler useCase) =>
                    await useCase.HandleAsync(request, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Events")
            .RequireAuthorization()
            .WithOpenApi();
    }
}