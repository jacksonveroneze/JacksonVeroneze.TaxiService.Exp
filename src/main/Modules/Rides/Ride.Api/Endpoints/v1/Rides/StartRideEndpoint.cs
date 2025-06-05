using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Ride.Application.v1.Ride.Start;

namespace TaxiService.Modules.Ride.Api.Endpoints.v1.Rides;

internal static class StartRideEndpoint
{
    public static RouteGroupBuilder AddStart(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/start", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                StartRideCommand command = new(id);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
