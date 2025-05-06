using System.Diagnostics;
using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Drivers.Application.v1.DriverLocation.Update;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

internal static class UpdateLocationDriverEndpoint
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public static RouteGroupBuilder SetLocationCommand(
        this RouteGroupBuilder builder)
    {
        builder.MapPost("{id:guid:required}/location", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                [FromBody] Request request,
                CancellationToken cancellationToken) =>
            {
                using Activity? activity = ActivitySource.StartActivity();

                UpdateCommand command = new(
                    id, request.Latitude, request.Longitude);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPost();
            })
            .AddDefaultResponseEndpoints();

        return builder;
    }

    private sealed record Request(
        float Latitude,
        float Longitude);
}
