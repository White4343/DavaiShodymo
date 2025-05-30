using DavaiShodymo.EndpointHelper;
using System.Security.Claims;

namespace DavaiShodymo.Events.CreateEvent;

public class CreateEventEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events/create",
                async (HttpContext httpContext, CreateEventCommand request, CreateEventHandler useCase, ClaimsPrincipal user) =>
                    await useCase.HandleAsync(request, user, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Events")
            .RequireAuthorization()
            .WithOpenApi();
    }
}