using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Driver.Application.v1.DriverAvailability.Update;
using TaxiService.Modules.Driver.Domain.Enums;

namespace TaxiService.Modules.Driver.Api.Endpoints.v1.Drivers.DriverAvailability;

internal static class UpdateDriverAvailabilityEndpoint
{
    public static RouteGroupBuilder UpdateAvailability(
        this RouteGroupBuilder builder)
    {
        builder.MapPost("{id:guid:required}/availability", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                [FromBody] Request request,
                CancellationToken cancellationToken) =>
            {
                UpdateDriverAvailabilityCommand command = new(
                    id, request.Status);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .AddDefaultResponseEndpoints();

        return builder;
    }

    private sealed record Request(
        DriverAvailabilityStatus Status);
}
