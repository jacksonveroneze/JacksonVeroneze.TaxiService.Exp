using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Ride.Application.v1.Ride.Finish;

namespace TaxiService.Modules.Ride.Api.Endpoints.v1.Rides;

internal static class FinishRideEndpoint
{
    public static RouteGroupBuilder AddFinish(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/finish", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                FinishRideCommand command = new(id);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
