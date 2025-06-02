using DavaiShodymo.EndpointHelper;
using DavaiShodymo.EventEnrolments.CreateEnrolment;
using System.Security.Claims;

namespace DavaiShodymo.EventEnrolments.UpdateEnrolment;

public class UpdateEnrolmentEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("enrolments/update",
                async (HttpContext httpContext, UpdateEnrolmentCommand request, UpdateEnrolmentHandler useCase, ClaimsPrincipal user) =>
                    await useCase.HandleAsync(request, user, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Enrolments")
            .RequireAuthorization()
            .WithOpenApi();
    }
}