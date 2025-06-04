using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.DriverLocation.Update;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers.DriverLocation;

internal static class UpdateDriverLocationEndpoint
{
    public static RouteGroupBuilder UpdateLocationCommand(
        this RouteGroupBuilder builder)
    {
        builder.MapPost("{id:guid:required}/location", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                [FromBody] Request request,
                CancellationToken cancellationToken) =>
            {
                UpdateDriverLocationCommand command = new(
                    id, request.Latitude, request.Longitude);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .AddDefaultResponseEndpoints();

        return builder;
    }

    private sealed record Request(
        float Latitude,
        float Longitude);
}
