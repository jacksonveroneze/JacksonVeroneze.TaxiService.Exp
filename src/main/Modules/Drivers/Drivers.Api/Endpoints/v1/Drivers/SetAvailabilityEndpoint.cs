using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers;

internal static class SetAvailabilityEndpoint
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public static RouteGroupBuilder SetAvailability(
        this RouteGroupBuilder builder)
    {
        builder.MapPost("{id:guid:required}/availability", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                [FromBody] Request request,
                CancellationToken cancellationToken) =>
            {
                using Activity? activity = ActivitySource.StartActivity();

                SetAvailabilityCommand command = new(id, request.Status);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPost();
            })
            .AddDefaultResponseEndpoints();

        return builder;
    }

    private record Request(DriverAvailabilityStatus Status);
}