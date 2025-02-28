using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

internal static class UpdateLocationEndpoint
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public static RouteGroupBuilder AddUpdateLocationCommand(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/location", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                [FromBody] Request request,
                CancellationToken cancellationToken) =>
            {
                using Activity? activity = ActivitySource.StartActivity();

                UpdateLocationCommand command = new(
                    id, request.Latitude, request.Longitude);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPut();
            })
            .AddDefaultResponseEndpoints();

        return builder;
    }

    private record Request(float Latitude, float Longitude);
}