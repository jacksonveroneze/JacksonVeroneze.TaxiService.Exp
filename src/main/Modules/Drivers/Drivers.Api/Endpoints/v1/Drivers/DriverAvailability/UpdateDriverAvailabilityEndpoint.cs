using System.Diagnostics;

using JacksonVeroneze.NET.Result;

using TaxiService.Modules.Drivers.Application.v1.DriverAvailability.Update;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers.DriverAvailability;

internal static class UpdateDriverAvailabilityEndpoint
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public static RouteGroupBuilder UpdateAvailability(
        this RouteGroupBuilder builder)
    {
        builder.MapPost("{id:guid:required}/availability", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                [FromBody] Request request,
                CancellationToken cancellationToken) =>
            {
                using Activity? activity = ActivitySource.StartActivity();

                UpdateDriverAvailabilityCommand command = new(id, request.Status);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPost();
            })
            .AddDefaultResponseEndpoints();

        return builder;
    }

    private sealed record Request(
        DriverAvailabilityStatus Status);
}
