using DavaiShodymo.EndpointHelper;

namespace DavaiShodymo.Users.GetProfile;

public class GetProfileEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/profile",
                async (HttpContext httpContext, GetProfileCommand command, GetProfileHandler useCase) =>
                    await useCase.HandleAsync(command.Id, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Users")
            .WithOpenApi();
    }
}