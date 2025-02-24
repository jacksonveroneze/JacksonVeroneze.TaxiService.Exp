using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers.DriverAvailability;

internal static class UpdateAvailabilityEndpoint
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public static RouteGroupBuilder AddUpdateAvailability(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/availability", async (
                [FromServices] ISender mediator,
                [FromBody] UpdateAvailabilityCommand command,
                CancellationToken cancellationToken) =>
            {
                using Activity? activity = ActivitySource.StartActivity();

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPut();
            })
            .AddDefaultResponseEndpoints();

        return builder;
    }
}