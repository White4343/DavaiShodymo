using DavaiShodymo.EndpointHelper;

namespace DavaiShodymo.EventEnrolments.GetEnrolmentByUser;

public class GetEnrolmentByUserEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("enrolments/getbyuser/{id}",
                async (HttpContext httpContext, int id, GetEnrolmentByUserHandler useCase) =>
                    await useCase.HandleAsync(id, httpContext.RequestAborted).ConfigureAwait(false))
            .WithTags("Enrolments")
            .WithOpenApi();
    }
}