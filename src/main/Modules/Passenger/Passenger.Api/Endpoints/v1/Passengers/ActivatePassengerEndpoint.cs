using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Activate;

namespace TaxiService.Modules.Passenger.Api.Endpoints.v1.Passengers;

internal static class ActivatePassengerEndpoint
{
    public static RouteGroupBuilder AddActivate(
        this RouteGroupBuilder builder)
    {
        builder.MapPut("{id:guid:required}/activate", async (
                [FromServices] ISender mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                ActivatePassengerCommand command = new(id);

                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.ToIResult();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return builder;
    }
}
