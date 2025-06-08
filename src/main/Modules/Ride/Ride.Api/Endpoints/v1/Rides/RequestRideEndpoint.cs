using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Ride.Application.v1.Ride.Request;

namespace TaxiService.Modules.Ride.Api.Endpoints.v1.Rides;

internal static class RequestRideEndpoint
{
    public static RouteGroupBuilder AddRequest(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] RequestRideCommand command,
                [FromServices] LinkGenerator linkGenerator,
                HttpContext httpContext,
                CancellationToken cancellationToken) =>
            {
                Result<RequestRideCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.ToCreatedResultFromRoute(
                    linkGenerator,
                    httpContext,
                    RouteNames.GetRideById,
                    response.Value?.Data?.Id!
                );
            })
            .Produces<RequestRideCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
