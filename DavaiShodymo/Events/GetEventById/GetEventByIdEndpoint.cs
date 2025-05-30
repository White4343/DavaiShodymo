using DavaiShodymo.EndpointHelper;

namespace DavaiShodymo.Events.GetEventById;

public class GetEventByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events/getone",
                async (HttpContext httpContext, GetEventByIdCommand request, GetEventByIdHandler useCase) =>
                    await useCase.HandleAsync(request, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Events")
            .RequireAuthorization()
            .WithOpenApi();
    }
}