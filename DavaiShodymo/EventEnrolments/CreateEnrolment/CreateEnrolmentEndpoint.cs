using DavaiShodymo.EndpointHelper;
using System.Security.Claims;

namespace DavaiShodymo.EventEnrolments.CreateEnrolment;

public class CreateEnrolmentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("enrolments/create",
                async (HttpContext httpContext, CreateEnrolmentCommand request, CreateEnrolmentHandler useCase, ClaimsPrincipal user) =>
                    await useCase.HandleAsync(request, user, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Enrolments")
            .RequireAuthorization()
            .WithOpenApi();
    }
}