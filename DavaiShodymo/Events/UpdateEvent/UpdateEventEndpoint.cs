using DavaiShodymo.EndpointHelper;
using System.Security.Claims;

namespace DavaiShodymo.Events.UpdateEvent;

public class UpdateEventEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events/update",
                async (HttpContext httpContext, UpdateEventCommand request, UpdateEventHandler useCase, ClaimsPrincipal user) =>
                    await useCase.HandleAsync(request, user, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Events")
            .RequireAuthorization()
            .WithOpenApi();
    }
}