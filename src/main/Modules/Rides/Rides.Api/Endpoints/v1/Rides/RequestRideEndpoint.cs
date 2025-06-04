using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Rides.Application.v1.Ride.Request;

namespace TaxiService.Modules.Rides.Api.Endpoints.v1.Rides;

internal static class RequestRideEndpoint
{
    public static RouteGroupBuilder AddRequest(
        this RouteGroupBuilder builder)
    {
        builder.MapPost(string.Empty, async (
                [FromServices] ISender mediator,
                [FromBody] RequestRideCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<RequestRideCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces<RequestRideCommandResponse>()
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
