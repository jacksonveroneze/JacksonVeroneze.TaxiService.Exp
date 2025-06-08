using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Ride.Application.v1.Ride.Accept;

namespace TaxiService.Modules.Ride.Api.Endpoints.v1.Rides;

internal static class AcceptRideEndpoint
{
    public static RouteGroupBuilder AddAccept(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/accept", async (
                [FromServices] ISender mediator,
                [AsParameters] AcceptRideCommand command,
                CancellationToken cancellationToken) =>
            {
                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
