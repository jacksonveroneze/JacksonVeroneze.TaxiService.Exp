using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;

namespace TaxiService.Modules.Drivers.Api.Endpoints.v1.Drivers.DriverAvailability;

internal static class UpdateLocationEndpoint
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public static RouteGroupBuilder AddUpdateLocationCommand(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/location", async (
                [FromServices] ISender mediator,
                [FromBody] UpdateLocationCommand command,
                CancellationToken cancellationToken) =>
            {
                using Activity? activity = ActivitySource.StartActivity();

                Result response = await mediator.Send
                    (command, cancellationToken);

                return response.MatchPut();
            })
            .AddDefaultResponseEndpoints();

        return builder;
    }
}