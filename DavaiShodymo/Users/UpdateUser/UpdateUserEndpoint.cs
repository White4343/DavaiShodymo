using System.Security.Claims;
using DavaiShodymo.EndpointHelper;

namespace DavaiShodymo.Users.UpdateUser;

public class UpdateUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/update",
                async (HttpContext httpContext, UpdateUserCommand request, UpdateUserHandler useCase, ClaimsPrincipal user) =>
                    await useCase.HandleAsync(request, user, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Users")
            .RequireAuthorization()
            .WithOpenApi();
    }
}