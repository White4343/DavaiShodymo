using DavaiShodymo.EndpointHelper;

namespace DavaiShodymo.Users.Register;

public sealed class RegisterUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register",
                async (HttpContext httpContext, RegisterCommand request, RegisterHandler useCase) =>
                    await useCase.HandleAsync(request, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Users")
            .WithOpenApi();
    }
}